namespace Barber.Application.DTOs.Responses.Token;

public record GenerateTokensResponseDTO
(
    string AccessToken,
    string RefreshToken
);