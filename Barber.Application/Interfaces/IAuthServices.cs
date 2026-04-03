using Barber.Application.DTOs.Requests.Auth;
using Barber.Application.DTOs.Responses;
using Barber.Application.DTOs.Responses.Token;

namespace Barber.Application.Interfaces;

public interface IAuthServices
{
    Task<DefaultResponseDTO> RegisterUserAndBarbershop(RegisterUserAndBarbershopDTO registerUserAndBarbershopDTO);
    Task<GenerateTokensResponseDTO> Login(LoginDTO loginDTO);
}
