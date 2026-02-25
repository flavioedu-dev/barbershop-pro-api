using Barber.Application.DTOs.Barbershop;
using Barber.Application.Interfaces;
using Barber.Domain.Entities;

namespace Barber.Application.Services;

public class BarbershopServices : IBarbershopServices
{
    public Task<Barbershop> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Barbershop> Create(CreateBarbershopDTO createBarbershopDTO)
    {
        throw new NotImplementedException();
    }
}
