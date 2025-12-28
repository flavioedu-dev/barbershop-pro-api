using Barber.API.Extensions;
using Barber.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddOpenApi();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen(c => c.EnableAnnotations());

services.AddDbContext<BarberDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Barber.Infrastructure")
    )
);
services.AddInfrastructureDI();
services.AddApplicationDI();
services.RegisterMappings();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AddMapEndpoints();

app.Run();