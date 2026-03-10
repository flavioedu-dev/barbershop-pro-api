using Barber.API.Models.Auth;
using Barber.Application.DTOs.Requests.Auth;
using Barber.Domain.Entities;
using Mapster;

namespace Barber.API.Mappings;

public static class AuthMappingConfigurations
{
    public static void AuthRegisterMappings(this IServiceCollection services)
    {
        TypeAdapterConfig<RegisterUserAndBarbershopModel, RegisterUserAndBarbershopDTO>.NewConfig();

        TypeAdapterConfig<RegisterUserAndBarbershopDTO, Barbershop>.NewConfig()
            .Map(dest => dest.Name, src => src.BarbershopName)
            .Map(dest => dest.PhoneNumber, src => src.BarbershopPhoneNumber);
    }
}
