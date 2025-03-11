using Application.DTOs.Product;
using Application.Interfaces.Infrastructure.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository(DbContext context) : BaseRepository<Product>(context), IProductRepository
    {
        public async Task<ICollection<ProductDto>> GetAllWithDetail()
        {
            return await _DbSet
                .Include(x => x.Category)
                .Select(x => new ProductDto()
                {
                    Category = x.Category.Categoryname,
                    Discontinued = x.Discontinued,
                    Productid = x.Productid,
                    Productname = x.Productname,
                    Supplierid = x.Supplierid,
                    Unitprice = x.Unitprice
                }).ToListAsync();
        }
    }
}
