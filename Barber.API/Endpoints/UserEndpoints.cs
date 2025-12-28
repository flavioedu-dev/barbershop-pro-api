using Barber.API.Models.Users;
using Barber.Domain.DTOs.Users;
using Barber.Domain.Interfaces.Application.Services;
using Mapster;
using Swashbuckle.AspNetCore.Annotations;

namespace Barber.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var userEndpoints = app.MapGroup("/api/users").WithTags("Users");

        userEndpoints.MapGet("/", () => Results.Ok("Get all users"));
        userEndpoints.MapPost("/", CreateUser);
    }

    [SwaggerOperation(Summary = "Cria novo usuário.")]
    public static async Task<IResult> CreateUser([AsParameters] CreateUserModel createUserModel, IUserServices userServices)
    {
        var createUserDTO = createUserModel.Adapt<CreateUserDTO>();

        var createdUser = await userServices.CreateUser(createUserDTO);

        return Results.Ok(createdUser);
    }
}
