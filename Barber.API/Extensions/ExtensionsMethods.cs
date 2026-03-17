using Barber.API.Endpoints;
using Barber.API.Mappings;
using Barber.API.Middlewares;
using Barber.API.Models.Auth;
using Barber.API.Validations.Auth;
using Barber.Application.Interfaces;
using Barber.Application.Services;
using Barber.Domain.Interfaces.Repositories;
using Barber.Infrastructure.Repositories;
using Barber.Infrastructure.Repositories.Base;
using FluentValidation;
using System.Reflection;

namespace Barber.API.Extensions;

public static class ExtensionsMethods
{
    public static void AddApplicationDI(this IServiceCollection services)
    {
        services.AddScoped<IAuthServices, AuthServices>();
        services.AddScoped<IBarbershopServices, BarbershopServices>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public static void AddInfrastructureDI(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBarbershopRepository, BarbershopRepository>();
    }

    public static void AddMapEndpoints(this WebApplication app)
    {
        app.MapAuthEndpoints();
        app.MapBarbershopEndpoints();
    }

    public static void AddMiddlewares(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static void RegisterMappings(this IServiceCollection services)
    {
        services.AuthRegisterMappings();
    }
}
