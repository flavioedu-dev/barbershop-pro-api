using Barber.Application.DTOs.Requests.Auth;
using Barber.Application.DTOs.Responses;
using Barber.Application.DTOs.Responses.Token;
using Barber.Application.Interfaces;
using Barber.Domain.Entities;
using Barber.Domain.Enums;
using Barber.Domain.Exceptions;
using Barber.Domain.Interfaces.Repositories;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace Barber.Application.Services;

public class AuthServices : IAuthServices
{
    private readonly IUserRepository _userRepository;
    private readonly IBarbershopRepository _barbershopRepository;
    private readonly ITokenService _tokenService;

    public AuthServices(IUserRepository userRepository, IBarbershopRepository barbershopRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _barbershopRepository = barbershopRepository;
        _tokenService = tokenService;
    }

    public async Task<DefaultResponseDTO> RegisterUserAndBarbershop(RegisterUserAndBarbershopDTO registerUserAndBarbershopDTO)
    {
        if (registerUserAndBarbershopDTO.Password != registerUserAndBarbershopDTO.ConfirmPassword)
            throw new CustomResponseException("Senhas não correspondem,.", 400);

        var existsUser = await _userRepository.GetAsync(x => x.Email == registerUserAndBarbershopDTO.Email);

        if (existsUser is not null)
            throw new CustomResponseException("Email indisponível.", 400);

        Barbershop newBarbershop = registerUserAndBarbershopDTO.Adapt<Barbershop>();
        newBarbershop.Slug = $"{newBarbershop.Name.ToLower().Replace(" ", "-")}-{Guid.NewGuid().ToString().Substring(0, 8)}";

        await _barbershopRepository.CreateAsync(newBarbershop);
        await _barbershopRepository.SaveAsync();

        var newUser = registerUserAndBarbershopDTO.Adapt<User>();
        newUser.Password = new PasswordHasher<User>().HashPassword(newUser, newUser.Password);
        newUser.BarbershopId = newBarbershop.Id;
        newUser.Role = UserRole.Admin;

        await _userRepository.CreateAsync(newUser);
        await _userRepository.SaveAsync();

        return new DefaultResponseDTO(true, "Usuário cadastrado com sucesso.");
    }

    public async Task<GenerateTokensResponseDTO> Login(LoginDTO loginDTO)
    {
        var registeredUser = await _userRepository.GetAsync(x => x.Email == loginDTO.Email)
            ?? throw new CustomResponseException("Credenciais inválidas.", 401);

        var passwordVerificationResult = new PasswordHasher<User>().VerifyHashedPassword(registeredUser, registeredUser.Password, loginDTO.Password);

        if (passwordVerificationResult != PasswordVerificationResult.Success)
            throw new CustomResponseException("Credenciais inválidas.", 401);

        var generatedTokens = await _tokenService.GenerateTokens(registeredUser.Id, registeredUser.Email, registeredUser.Role);

        return generatedTokens;
    }
}
