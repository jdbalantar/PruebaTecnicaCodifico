using Application.DTOs.Order;
using Application.Interfaces.Infrastructure.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository(DbContext context) : BaseRepository<Order>(context), IOrderRepository
    {
        public async Task<ICollection<OrderDto>> GetOrdersByCustomer(int customerId)
        {
            return await _DbSet
                .Where(o => o.Custid == customerId)
                .Select(o => new OrderDto
                {
                    Orderid = o.Orderid,
                    Custid = o.Custid,
                    Empid = o.Empid,
                    Orderdate = o.Orderdate,
                    Requireddate = o.Requireddate,
                    Shippeddate = o.Shippeddate,
                    Shipperid = o.Shipperid,
                    Freight = o.Freight,
                    Shipname = o.Shipname,
                    Shipaddress = o.Shipaddress,
                    Shipcity = o.Shipcity,
                    Shipregion = o.Shipregion,
                    Shippostalcode = o.Shippostalcode,
                    Shipcountry = o.Shipcountry
                })
                .ToListAsync();
        }
    }
}
