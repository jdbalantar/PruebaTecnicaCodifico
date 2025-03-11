using Application.Interfaces.Infrastructure.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ShipperRepository(DbContext context) : BaseRepository<Shipper>(context), IShipperRepository
    {
    }
}
