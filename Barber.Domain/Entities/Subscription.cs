using Barber.Domain.Entities.Base;

namespace Barber.Domain.Entities;

public class Subscription : BaseEntity
{
    public required int UserId { get; set; }
    public required User? User { get; set; }
    public required int PriceId { get; set; } 
    public required bool IsActive { get; set; }
}
