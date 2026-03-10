using Barber.API.Models.Auth;
using Barber.Application.DTOs.Requests.Auth;
using Barber.Application.Interfaces;
using Mapster;
using Swashbuckle.AspNetCore.Annotations;

namespace Barber.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var authEndpoints = app.MapGroup("/api/auth").WithTags("auth");

        authEndpoints.MapPost("/register", RegisterUserAndBarbershop);
    }

    [SwaggerOperation(Summary = "Cadastrar novo usuário e sua barbearia.")]
    public static async Task<IResult> RegisterUserAndBarbershop([AsParameters] RegisterUserAndBarbershopModel registerUserAndBarbershopModel, IAuthServices authServices)
    {
        var registerUserAndBarbershopDTO = registerUserAndBarbershopModel.Adapt<RegisterUserAndBarbershopDTO>();

        var registeredUser = await authServices.RegisterUserAndBarbershop(registerUserAndBarbershopDTO);

        return Results.Ok(registeredUser);
    }
}
