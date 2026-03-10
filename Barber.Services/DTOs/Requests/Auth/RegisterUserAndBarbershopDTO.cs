namespace Barber.Application.DTOs.Requests.Auth;

public record RegisterUserAndBarbershopDTO(
    string Name,
    string Email,
    string Password,
    string BarbershopName,
    string BarbershopPhoneNumber
);
