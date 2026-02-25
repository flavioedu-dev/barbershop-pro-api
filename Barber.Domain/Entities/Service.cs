using Barber.Domain.Entities.Base;

namespace Barber.Domain.Entities;

public class Service : BaseEntity
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int DurationInMinutes { get; set; }
    public int BarbershopId { get; set; }
    public Barbershop? Barbershop { get; set; }
}
