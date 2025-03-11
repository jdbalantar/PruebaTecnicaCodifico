using Application.DTOs.Customer;
using Domain.Entities;

namespace Application.Interfaces.Infrastructure.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<ICollection<CustomerWithOrderHistoryDto>> GetCustomersWithOrderHistory();
    }
}
