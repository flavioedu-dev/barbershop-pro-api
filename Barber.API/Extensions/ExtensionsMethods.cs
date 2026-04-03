using Barber.API.Endpoints;
using Barber.API.Mappings;
using Barber.API.Middlewares;
using Barber.Application.Configurations;
using Barber.Application.Interfaces;
using Barber.Application.Services;
using Barber.Domain.Interfaces.Repositories;
using Barber.Infrastructure.Repositories;
using Barber.Infrastructure.Repositories.Base;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Barber.API.Extensions;

public static class ExtensionsMethods
{
    public static void AddApplicationDI(this IServiceCollection services)
    {
        services.AddScoped<IAuthServices, AuthServices>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IBarbershopServices, BarbershopServices>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public static void AddInfrastructureDI(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBarbershopRepository, BarbershopRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
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

    public static void AddJwt(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        var key = Encoding.UTF8.GetBytes(jwtSettings!.SecretKey);


        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });
    }

    public static void RegisterMappings(this IServiceCollection services)
    {
        services.AuthRegisterMappings();
    }
}
