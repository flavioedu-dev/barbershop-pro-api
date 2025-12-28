using Barber.API.Endpoints;
using Barber.Application.Services.Users;
using Barber.Domain.Interfaces.Application.Services;
using Barber.Domain.Interfaces.Infrastructure.Repositories;
using Barber.Infrastructure.Mapping;
using Barber.Infrastructure.Repositories;
using Barber.Infrastructure.Repositories.Base;

namespace Barber.API.Extensions;

public static class ExtensionsMethods
{
    public static void AddApplicationDI(this IServiceCollection services)
    {
        services.AddScoped<IUserServices, UserServices>();
    }

    public static void AddInfrastructureDI(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static void AddMapEndpoints(this WebApplication app)
    {
        app.MapUserEndpoints();
    }

    public static void RegisterMappings(this IServiceCollection services)
    {
        services.UserRegisterMappings();
    }
}
