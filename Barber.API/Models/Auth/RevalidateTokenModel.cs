namespace Barber.API.Models.Auth;

public class RevalidateTokenModel
{
    public required string RefreshToken { get; set; }
}
