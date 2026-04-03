using Barber.Application.DTOs.Requests.Auth;
using Barber.Application.DTOs.Responses;

namespace Barber.Application.Interfaces;

public interface IAuthServices
{
    Task<DefaultResponseDTO> RegisterUserAndBarbershop(RegisterUserAndBarbershopDTO registerUserAndBarbershopDTO);
    Task<DefaultResponseDTO> Login(LoginDTO loginDTO);
}
