using Barber.API.Endpoints;
using Barber.API.Mappings;
using Barber.Application.Interfaces;
using Barber.Application.Services;
using Barber.Domain.Interfaces.Repositories;
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
