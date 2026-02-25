using Barber.Domain.Entities.Base;

namespace Barber.Domain.Entities;

public class Client : BaseEntity
{
    public required string Name { get; set; }
    public string? Email { get; set; }
    public required string PhoneNumber { get; set; }
    public int BarbershopId { get; set; }
    public Barbershop? Barbershop { get; set; }
}
