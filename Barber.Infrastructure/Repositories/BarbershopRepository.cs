using Barber.Domain.Entities;
using Barber.Domain.Interfaces.Repositories;
using Barber.Infrastructure.Contexts;
using Barber.Infrastructure.Repositories.Base;

namespace Barber.Infrastructure.Repositories;

public class BarbershopRepository : BaseRepository<Barbershop>, IBarbershopRepository
{
    public BarbershopRepository(BarberDbContext context) : base(context)
    {
    }
}
