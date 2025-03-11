using Application.DTOs.Customer;
using Application.Interfaces.Infrastructure.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository(DbContext context) : BaseRepository<Customer>(context), ICustomerRepository
    {
        public async Task<ICollection<CustomerWithOrderHistoryDto>> GetCustomersWithOrderHistory()
        {
            return await _DbSet
                .Include(c => c.Orders)
                .Select(x => new CustomerWithOrderHistoryDto()
                {
                    Address = x.Address,
                    City = x.City,
                    Companyname = x.Companyname,
                    Contactname = x.Contactname,
                    Contacttitle = x.Contacttitle,
                    Country = x.Country,
                    Custid = x.Custid,
                    Fax = x.Fax,
                    Phone = x.Phone,
                    Postalcode = x.Postalcode,
                    Region = x.Region,
                    LastOrderDate = x.Orders.Max(o => (DateTime?)o.Orderdate),
                    NextPossibleOrderDate = x.Orders.Max(o => (DateTime?)o.Orderdate).HasValue
                        ? x.Orders.Max(o => (DateTime?)o.Orderdate)!.Value.AddDays(30)
                        : null
                }).ToListAsync();
        }

    }
}
