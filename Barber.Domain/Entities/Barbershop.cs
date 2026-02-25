using Barber.Domain.Entities.Base;

namespace Barber.Domain.Entities;

public class Barbershop : BaseEntity
{
    public required string Name { get; set; }
    public required string slug { get; set; }
    public required string PhoneNumber { get; set; }
    public required TimeOnly startHour { get; set; }
    public required TimeOnly endHour { get; set; }
}
