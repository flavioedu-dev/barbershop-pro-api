namespace Barber.API.Models.Auth;

public record RegisterUserAndBarbershopModel
(
    string Name,
    string Email,
    string Password,
    string BarbershopName,
    string BarbershopPhoneNumber
);
