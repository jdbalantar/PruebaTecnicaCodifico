using Application.DTOs;
using Application.DTOs.Order;
using Application.Interfaces.Infrastructure.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Order.Queries
{
    public class GetOrdersByCustomerQuery : IRequest<Result<ICollection<OrderDto>>>
    {
        [Required(ErrorMessage = "Customer Id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Customer Id must be greater than 0")]
        public int CustomerId { get; set; }
    }

    public class GetOrdersByCustomerQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetOrdersByCustomerQuery, Result<ICollection<OrderDto>>>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<Result<ICollection<OrderDto>>> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
        {
            bool exists = await unitOfWork.CustomerRepository.Exists(x => x.Custid == request.CustomerId, cancellationToken);
            if (!exists)
            {
                return Result<ICollection<OrderDto>>.NotFound($"Customer with id {request.CustomerId} does not exist.");
            }

            return Result<ICollection<OrderDto>>.Ok(await unitOfWork.OrderRepository.GetOrdersByCustomer(request.CustomerId));
        }
    }
}
