namespace Barber.Application.DTOs.Auth;

public record RegisterUserAndBarbershopDTO(
    string Name,
    string Email,
    string Password,
    string BarbershopName,
    string BarbershopPhoneNumber
);
