using Barber.Domain.Entities.Base;

namespace Barber.Domain.Entities;

public class Barber : BaseEntity
{
    public required string Name { get; set; }
    public int BarbershopId { get; set; }
    public Barbershop? Barbershop { get; set; }
}
