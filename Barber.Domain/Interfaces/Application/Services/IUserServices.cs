using Barber.Domain.DTOs.Users;
using Barber.Domain.Entities;

namespace Barber.Domain.Interfaces.Application.Services;

public interface IUserServices
{
    Task<User> CreateUser(CreateUserDTO createUserModel);
}
