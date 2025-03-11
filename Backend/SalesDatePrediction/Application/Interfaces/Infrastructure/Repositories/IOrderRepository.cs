using Application.DTOs.Order;
using Domain.Entities;

namespace Application.Interfaces.Infrastructure.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<ICollection<OrderDto>> GetOrdersByCustomer(int customerId);
    }
}
