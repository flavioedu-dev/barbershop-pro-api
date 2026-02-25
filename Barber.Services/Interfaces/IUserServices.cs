using Barber.Application.DTOs.Users;
using Barber.Domain.Entities;

namespace Barber.Application.Interfaces;

public interface IUserServices
{
    Task<User> CreateUser(CreateUserDTO createUserModel);
}
