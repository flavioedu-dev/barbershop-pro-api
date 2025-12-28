using Barber.API.Models.Users;
using Barber.Domain.DTOs.Users;
using Mapster;

namespace Barber.Infrastructure.Mapping;

public static class UserMappingConfigurations
{
    public static void UserRegisterMappings(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateUserModel, CreateUserDTO>.NewConfig();
    }
}
