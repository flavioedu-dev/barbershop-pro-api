using Barber.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Barber.Infrastructure.Contexts;

public class BarberDbContext(DbContextOptions<BarberDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; }
    public DbSet<Subscription> Subscriptions { get; }
    public DbSet<Haircut> Haircuts { get; }
    public DbSet<Service> Services { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<User>()
            .HasOne(u => u.Subscription)
            .WithOne(s => s.User)
            .HasForeignKey<User>(u => u.SubscriptionId);

        modelBuilder.Entity<Subscription>()
            .HasOne(s => s.User)
            .WithOne(u => u.Subscription)
            .HasForeignKey<Subscription>(s => s.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Haircuts)
            .WithOne(h => h.User)
            .HasForeignKey(h => h.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Services)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId);

        modelBuilder.Entity<Haircut>()
            .HasMany(h => h.Services)
            .WithOne(s => s.Haircut)
            .HasForeignKey(s => s.HaircutId);

        base.OnModelCreating(modelBuilder);
    }
}