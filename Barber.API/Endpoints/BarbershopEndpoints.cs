using Barber.API.Models.Barbershops;
using Barber.Application.DTOs.Barbershop;
using Barber.Application.Interfaces;
using Mapster;

namespace Barber.API.Endpoints;

public static class BarbershopEndpoints
{
    public static void MapBarbershopEndpoints(this IEndpointRouteBuilder app)
    {
        var barbershopEndpoints = app.MapGroup("/api/barbershops").WithTags("Barbershops");

        barbershopEndpoints.MapPost("/", CreateBarbershop);
    }

    public static async Task<IResult> CreateBarbershop(CreateBarbershopModel createBarbershopModel, IBarbershopServices barbershopServices)
    {
        CreateBarbershopDTO createBarbershopDTO = createBarbershopModel.Adapt<CreateBarbershopDTO>();

        var createdBarbershop = await barbershopServices.Create(createBarbershopDTO);

        return Results.Ok(createdBarbershop);
    }
}
