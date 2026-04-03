using Barber.Domain.Entities;
using Barber.Domain.Interfaces.Repositories;
using Barber.Infrastructure.Contexts;
using Barber.Infrastructure.Repositories.Base;

namespace Barber.Infrastructure.Repositories;

public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(BarberDbContext context) : base(context)
    {
    }
}

