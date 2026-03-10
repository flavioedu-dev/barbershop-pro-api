using Barber.Domain.Entities.Base;
using Barber.Domain.Enums;

namespace Barber.Domain.Entities;

public class Reservation : BaseEntity
{
    public DateTime StartAt { get; set; }
    public DateTime EndAt { get; set; }
    public required string ClientName { get; set; }
    public required string ClientPhone { get; set; }
    public ReservationStatus Status { get; set; }
    public int BarbershopId { get; set; }
    public Barbershop? Barbershop { get; set; }
    public int BarberId { get; set; }
    public Barber? Barber { get; set; }
    public int ServiceId { get; set; }
    public Service? Service { get; set; }
}
