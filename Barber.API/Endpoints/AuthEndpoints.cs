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
        var authEndpoints = app.MapGroup("/api/auth").WithTags("Auth");

        authEndpoints.MapPost("/register", RegisterUserAndBarbershop);
        authEndpoints.MapPost("/login", Login);
    }

    [SwaggerOperation(Summary = "Cadastrar novo usuário e sua barbearia.")]
    public static async Task<IResult> RegisterUserAndBarbershop([AsParameters] RegisterUserAndBarbershopModel registerUserAndBarbershopModel, IAuthServices authServices)
    {
        var registerUserAndBarbershopDTO = registerUserAndBarbershopModel.Adapt<RegisterUserAndBarbershopDTO>();

        var res = await authServices.RegisterUserAndBarbershop(registerUserAndBarbershopDTO);

        return Results.Ok(res);
    }

    [SwaggerOperation(Summary = "Realizar login de usuário.")]
    public static async Task<IResult> Login([AsParameters] LoginModel loginModel, IAuthServices authServices)
    {
        var loginDTO = loginModel.Adapt<LoginDTO>();

        var res = await authServices.Login(loginDTO);

        return Results.Ok(res);
    }
}
