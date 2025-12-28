using Barber.Domain.Entities.Base;

namespace Barber.Domain.Entities;

public class User : BaseEntity
{
    public int SubscriptionId { get; set; }
    public Subscription? Subscription { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? Adress { get; set; }
    public required string Password { get; set; }

    public ICollection<Haircut> Haircuts { get; set; } = [];
    public ICollection<Service> Services { get; set; } = [];
}
