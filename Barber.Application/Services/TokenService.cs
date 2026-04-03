using Barber.Application.Configurations;
using Barber.Application.DTOs.Responses.Token;
using Barber.Application.Interfaces;
using Barber.Domain.Entities;
using Barber.Domain.Enums;
using Barber.Domain.Exceptions;
using Barber.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Barber.Application.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings? _jwtSettings;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public TokenService(IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
    {
        _jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<GenerateTokensResponseDTO> GenerateTokens(int userId)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings!.SecretKey));

        var credentials = new SigningCredentials(
            key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var accessToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
            signingCredentials: credentials
        );

        string refreshTokenNoHash = GenerateSecureToken();

        var newRefreshToken = new RefreshToken
        {
            UserId = userId.ToString(),
            TokenHash = HashToken(refreshTokenNoHash),
            ExpirationDate = DateTime.UtcNow.AddDays(7),
        };

        await _refreshTokenRepository.CreateAsync(newRefreshToken);
        await _refreshTokenRepository.SaveAsync();

        return new GenerateTokensResponseDTO
        (
            AccessToken: new JwtSecurityTokenHandler().WriteToken(accessToken),
            RefreshToken: refreshTokenNoHash
        );
    }

    public async Task<bool> InvalidateRefreshToken(string refreshToken)
    {
        var hashedToken = HashToken(refreshToken);

        var refreshTokenExists = await _refreshTokenRepository.GetAsync(x => x.TokenHash == hashedToken && x.RevokedAt == null)
            ?? throw new CustomResponseException("RefreshToken inválido.", 404);

        refreshTokenExists.RevokedAt = DateTime.UtcNow;

        await _refreshTokenRepository.SaveAsync();

        return true;
    }
    public async Task<GenerateTokensResponseDTO> RevalidateJwt(string refreshToken)
    {
        var hashedToken = HashToken(refreshToken);

        var refreshTokenExists = await _refreshTokenRepository.GetAsync(x => x.TokenHash == hashedToken && x.RevokedAt == null)
           ?? throw new CustomResponseException("RefreshToken inválido.", 404);

        if (!refreshTokenExists.IsActive)
            throw new CustomResponseException("RefreshToken inválido.", 401);

        refreshTokenExists.RevokedAt = DateTime.UtcNow;

        var newTokens = await GenerateTokens(int.Parse(refreshTokenExists.UserId));

        return newTokens;
    }


    private static string GenerateSecureToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    private string HashToken(string token)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
        return Convert.ToBase64String(bytes);
    }
}
