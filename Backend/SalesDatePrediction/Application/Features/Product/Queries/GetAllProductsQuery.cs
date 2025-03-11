using Application.DTOs;
using Application.DTOs.Product;
using Application.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace Application.Features.Product.Queries
{
    public class GetAllProductsQuery : IRequest<Result<ICollection<ProductDto>>> { }

    public class GetAllProductsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllProductsQuery, Result<ICollection<ProductDto>>>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<Result<ICollection<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return Result<ICollection<ProductDto>>.Ok(await unitOfWork.ProductRepository.GetAllWithDetail());
        }
    }
}
