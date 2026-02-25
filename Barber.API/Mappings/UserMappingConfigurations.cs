using Barber.API.Models.Users;
using Barber.Application.DTOs.Users;
using Mapster;

namespace Barber.API.Mappings;

public static class UserMappingConfigurations
{
    public static void UserRegisterMappings(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateUserModel, CreateUserDTO>.NewConfig();
    }
}
