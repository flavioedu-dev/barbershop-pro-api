using Barber.Domain.Entities.Base;

namespace Barber.Domain.Entities;

public class Service : BaseEntity
{
    public required int UserId { get; set; }
    public User? User { get; set; }
    public required int HaircutId { get; set; }
    public Haircut? Haircut { get; set; }
    public required string Customer { get; set; }
}
