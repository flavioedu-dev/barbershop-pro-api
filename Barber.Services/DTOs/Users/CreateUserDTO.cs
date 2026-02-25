namespace Barber.Application.DTOs.Users;

public record CreateUserDTO(
    string Name,
    string Email,
    string Password
);
