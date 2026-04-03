using Barber.API.Models.Auth;
using Barber.Application.DTOs.Requests.Auth;
using Barber.Application.Interfaces;
using Barber.Domain.Exceptions;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Barber.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var authEndpoints = app.MapGroup("/api/auth").WithTags("Auth");

        authEndpoints.MapPost("/register", RegisterUserAndBarbershop);
        authEndpoints.MapPost("/login", Login);
        authEndpoints.MapPost("/logout", Logout);
        authEndpoints.MapPost("/revalidateToken", RevalidateToken);
    }

    [SwaggerOperation(Summary = "Cadastrar novo usuário e sua barbearia.")]
    public static async Task<IResult> RegisterUserAndBarbershop([FromBody] RegisterUserAndBarbershopModel registerUserAndBarbershopModel, IValidator<RegisterUserAndBarbershopModel> validator, IAuthServices authServices)
    {
        var validationResult = await validator.ValidateAsync(registerUserAndBarbershopModel);

        if (!validationResult.IsValid)
            throw new InputValidationException(validationResult);

        var registerUserAndBarbershopDTO = registerUserAndBarbershopModel.Adapt<RegisterUserAndBarbershopDTO>();

        var res = await authServices.RegisterUserAndBarbershop(registerUserAndBarbershopDTO);

        return Results.Ok(res);
    }

    [SwaggerOperation(Summary = "Realizar login de usuário.")]
    public static async Task<IResult> Login([FromBody] LoginModel loginModel, IValidator<LoginModel> validator, IAuthServices authServices)
    {
        var validationResult = await validator.ValidateAsync(loginModel);

        if (!validationResult.IsValid)
            throw new InputValidationException(validationResult);

        var loginDTO = loginModel.Adapt<LoginDTO>();

        var res = await authServices.Login(loginDTO);

        return Results.Ok(res);
    }

    [Authorize]
    [SwaggerOperation(Summary = "Realizar logout de usuário.")]
    public static async Task<IResult> Logout([FromBody] LogoutModel logoutModel, IValidator<LogoutModel> validator, IAuthServices authServices)
    {
        var validationResult = await validator.ValidateAsync(logoutModel);

        if (!validationResult.IsValid)
            throw new InputValidationException(validationResult);

        var res = await authServices.Logout(logoutModel.RefreshToken);

        return Results.Ok(res);
    }

    [SwaggerOperation(Summary = "Realizar renovação do token JWT.")]
    public static async Task<IResult> RevalidateToken([FromBody] RevalidateTokenModel revalidateTokenModel, IValidator<RevalidateTokenModel> validator, ITokenService tokenService)
    {
        var validationResult = await validator.ValidateAsync(revalidateTokenModel);

        if (!validationResult.IsValid)
            throw new InputValidationException(validationResult);

        var res = await tokenService.RevalidateJwt(revalidateTokenModel.RefreshToken);

        return Results.Ok(res);
    }
}
