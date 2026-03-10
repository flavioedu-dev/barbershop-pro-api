using Barber.Domain.Entities.Base;
using Barber.Domain.Enums;

namespace Barber.Domain.Entities;

public class User : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required UserRole Role { get; set; }
    public int BarbershopId { get; set; }
    public Barbershop? Barbershop { get; set; }
}
