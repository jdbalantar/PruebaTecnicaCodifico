using Application.DTOs.Product;
using Domain.Entities;

namespace Application.Interfaces.Infrastructure.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<ICollection<ProductDto>> GetAllWithDetail();
    }
}
