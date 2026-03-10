using Barber.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Contexts;

public class BarberDbContext(DbContextOptions<BarberDbContext> options) : DbContext(options)
{
    public DbSet<Barbershop> Barbershops { get; }
    public DbSet<User> Users { get; }
    public DbSet<Domain.Entities.Barber> Barbers { get; }
    public DbSet<Service> Services { get; }
    public DbSet<Reservation> Reservations { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}