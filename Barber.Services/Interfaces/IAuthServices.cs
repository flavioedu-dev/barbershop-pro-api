using Barber.Application.DTOs.Auth;
using Barber.Domain.Entities;

namespace Barber.Application.Interfaces;

public interface IAuthServices
{
    Task<User> RegisterUserAndBarbershop(RegisterUserAndBarbershopDTO registerUserAndBarbershopDTO);
}
