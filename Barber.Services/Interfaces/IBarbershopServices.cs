using Barber.Application.DTOs.Barbershop;
using Barber.Domain.Entities;

namespace Barber.Application.Interfaces;

public interface IBarbershopServices
{
    Task<Barbershop> GetById(int id);
    Task<Barbershop> Create(CreateBarbershopDTO createBarbershopDTO);
}
