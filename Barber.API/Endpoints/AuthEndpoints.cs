using Barber.API.Models.Auth;
using Barber.Application.DTOs.Requests.Auth;
using Barber.Application.Interfaces;
using Barber.Domain.Exceptions;
using FluentValidation;
using Mapster;
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
    }

    [SwaggerOperation(Summary = "Cadastrar novo usuário e sua barbearia.")]
    public static async Task<IResult> RegisterUserAndBarbershop([FromBody] RegisterUserAndBarbershopModel registerUserAndBarbershopModel, IValidator<RegisterUserAndBarbershopModel> validator, IAuthServices authServices)
    {
        var validationResult = await validator.ValidateAsync(registerUserAndBarbershopModel);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(err => err.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(e => e.ErrorMessage).ToArray()
                );

            throw new PropertyValidationException(errors);
        }

        var registerUserAndBarbershopDTO = registerUserAndBarbershopModel.Adapt<RegisterUserAndBarbershopDTO>();

        var res = await authServices.RegisterUserAndBarbershop(registerUserAndBarbershopDTO);

        return Results.Ok(res);
    }

    [SwaggerOperation(Summary = "Realizar login de usuário.")]
    public static async Task<IResult> Login([FromBody] LoginModel loginModel, IAuthServices authServices)
    {
        var loginDTO = loginModel.Adapt<LoginDTO>();

        var res = await authServices.Login(loginDTO);

        return Results.Ok(res);
    }
}
