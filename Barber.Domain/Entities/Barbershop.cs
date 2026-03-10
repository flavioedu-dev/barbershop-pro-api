using Barber.Domain.Entities.Base;

namespace Barber.Domain.Entities;

public class Barbershop : BaseEntity
{
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public required string PhoneNumber { get; set; }
    public required TimeOnly StartHour { get; set; } = new TimeOnly(8, 0);
    public required TimeOnly EndHour { get; set; } = new TimeOnly(18, 0);
}
