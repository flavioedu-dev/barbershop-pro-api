using Barber.Domain.Entities.Base;

namespace Barber.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public required string UserId { get; set; }
    public required string TokenHash { get; set; }

    public DateTime ExpirationDate { get; set; }

    public DateTime? RevokedAt { get; set; }
    public string? ReplacedByTokenHash { get; set; }

    public string? CreatedByIp { get; set; }
    public string? RevokedByIp { get; set; }

    public bool IsExpired => DateTime.UtcNow >= ExpirationDate;
    public bool IsActive => RevokedAt == null && !IsExpired;
}
