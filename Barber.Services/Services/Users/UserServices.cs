using Barber.Domain.DTOs.Users;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces.Application.Services;
using Barber.Domain.Interfaces.Infrastructure.Repositories;
using Mapster;

namespace Barber.Application.Services.Users;

public class UserServices : IUserServices
{
    private readonly IUserRepository _userRepository;

    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUser(CreateUserDTO createUserModel)
    {
        var existsUser = _userRepository.GetAsync(x => x.Email == createUserModel.Email);

        if (existsUser is not null)
            throw new Exception("Email de usuário já existente.");

        var newUser = createUserModel.Adapt<User>();

        await _userRepository.CreateAsync(newUser);
        await _userRepository.SaveAsync();

        return newUser;
    }
}
