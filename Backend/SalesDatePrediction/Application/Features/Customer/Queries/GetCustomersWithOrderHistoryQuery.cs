using Application.DTOs;
using Application.DTOs.Customer;
using Application.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace Application.Features.Customer.Queries
{
    public class GetCustomersWithOrderHistoryQuery : IRequest<Result<ICollection<CustomerWithOrderHistoryDto>>> { }

    public class GetCustomersWithOrderHistoryQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCustomersWithOrderHistoryQuery, Result<ICollection<CustomerWithOrderHistoryDto>>>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<Result<ICollection<CustomerWithOrderHistoryDto>>> Handle(GetCustomersWithOrderHistoryQuery request, CancellationToken cancellationToken)
        {
            return Result<ICollection<CustomerWithOrderHistoryDto>>.Ok(await unitOfWork.CustomerRepository.GetCustomersWithOrderHistory());
        }
    }
}
