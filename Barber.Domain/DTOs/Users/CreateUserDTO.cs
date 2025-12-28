namespace Barber.Domain.DTOs.Users;

public record CreateUserDTO(
    string Name,
    string Email,
    string Password
);
