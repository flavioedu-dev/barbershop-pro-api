using Barber.Application.DTOs.Responses.Token;
using Barber.Domain.Enums;

namespace Barber.Application.Interfaces;

public interface ITokenService
{
    Task<GenerateTokensResponseDTO> GenerateTokens(int userId);
    Task<bool> InvalidateRefreshToken(string refreshToken);
    Task<GenerateTokensResponseDTO> RevalidateJwt(string refreshToken);
}
