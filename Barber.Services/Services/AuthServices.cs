using Barber.Application.DTOs.Requests.Auth;
using Barber.Application.DTOs.Responses;
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

    public AuthServices(IUserRepository userRepository, IBarbershopRepository barbershopRepository)
    {
        _userRepository = userRepository;
        _barbershopRepository = barbershopRepository;
    }

    public async Task<DefaultResponseDTO> RegisterUserAndBarbershop(RegisterUserAndBarbershopDTO registerUserAndBarbershopDTO)
    {
        var existsUser = await _userRepository.GetAsync(x => x.Email == registerUserAndBarbershopDTO.Email);

        if (existsUser is not null)
            throw new CustomResponseException("Email indisponível.", 404);

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

    public async Task<DefaultResponseDTO> Login(LoginDTO loginDTO)
    {
        var registeredUser = await _userRepository.GetAsync(x => x.Email == loginDTO.Email)
            ?? throw new CustomResponseException("Email ou senha inválidos.", 404);

        var passwordVerificationResult = new PasswordHasher<User>().VerifyHashedPassword(registeredUser, registeredUser.Password, loginDTO.Password);

        if(passwordVerificationResult != PasswordVerificationResult.Success)
            throw new CustomResponseException("Email ou senha inválidos.", 404);

        return new DefaultResponseDTO(true, "Login realizado com sucesso.");
    }
}
