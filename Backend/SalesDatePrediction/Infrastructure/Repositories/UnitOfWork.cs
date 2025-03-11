using Application.Interfaces.Infrastructure.Repositories;
using Infrastructure.DataContext;

namespace Infrastructure.Repositories
{
    public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        private bool disposed;

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public Task Rollback()
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _dbContext.Dispose();
            }
            disposed = true;
        }

        #region Repositories

        private CustomerRepository? _customerRepository;
        public ICustomerRepository CustomerRepository { get { return _customerRepository ??= new CustomerRepository(_dbContext); } }

        private ProductRepository? _productRepository;
        public IProductRepository ProductRepository { get { return _productRepository ??= new ProductRepository(_dbContext); } }

        private OrderRepository? _orderRepository;
        public IOrderRepository OrderRepository { get { return _orderRepository ??= new OrderRepository(_dbContext); } }

        private ShipperRepository? _shipperRepository;
        public IShipperRepository ShipperRepository { get { return _shipperRepository ??= new ShipperRepository(_dbContext); } }

        private EmployeeRepository? _employeeRepository;
        public IEmployeeRepository EmployeeRepository { get { return _employeeRepository ??= new EmployeeRepository(_dbContext); } }

        #endregion
    }
}
