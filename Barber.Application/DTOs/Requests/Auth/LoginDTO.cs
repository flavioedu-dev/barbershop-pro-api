namespace Barber.Application.DTOs.Requests.Auth;

public record LoginDTO(
    string Email,
    string Password
);
