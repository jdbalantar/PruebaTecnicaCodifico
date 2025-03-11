namespace Application.Interfaces.Infrastructure.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task Rollback();

        #region Repositories

        ICustomerRepository CustomerRepository { get; }
        IProductRepository ProductRepository { get; }
        IOrderRepository OrderRepository { get; }
        IShipperRepository ShipperRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }

        #endregion
    }
}
