using Barber.Domain.Entities;
using Barber.Domain.Interfaces.Infrastructure.Repositories;
using Barber.Infrastructure.Contexts;
using Barber.Infrastructure.Repositories.Base;

namespace Barber.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(BarberDbContext context) : base(context)
    {
    }
}
