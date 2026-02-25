using Barber.Application.DTOs.Users;
using Barber.Application.Interfaces;
using Barber.Domain.Entities;
using Barber.Domain.Interfaces.Repositories;
using Mapster;

namespace Barber.Application.Services;

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
            throw new Exception("Email indisponível.");

        var newUser = createUserModel.Adapt<User>();

        await _userRepository.CreateAsync(newUser);
        await _userRepository.SaveAsync();

        return newUser;
    }
}
