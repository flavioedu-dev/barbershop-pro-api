using Barber.Domain.Entities.Base;

namespace Barber.Domain.Entities;

public class Haircut : BaseEntity
{
    public required int UserId { get; set; }
    public User? User { get; set; }
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public required bool IsActive { get; set; }

    public ICollection<Service> Services { get; set; } = [];
}
