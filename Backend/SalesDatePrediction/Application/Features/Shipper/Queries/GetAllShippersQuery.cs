using Application.DTOs;
using Application.DTOs.Shipper;
using Application.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace Application.Features.Shipper.Queries
{
    public class GetAllShippersQuery : IRequest<Result<ICollection<ShipperDto>>> { }

    public class GetAllShippersQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllShippersQuery, Result<ICollection<ShipperDto>>>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<Result<ICollection<ShipperDto>>> Handle(GetAllShippersQuery request, CancellationToken cancellationToken)
        {
            var shippers = await unitOfWork.ShipperRepository.GetAllAsync(cancellationToken);
            var shippersDto = shippers.Select(s => new ShipperDto
            {
                Companyname = s.Companyname,
                Phone = s.Phone,
                Shipperid = s.Shipperid,
            }).ToList();
            return Result<ICollection<ShipperDto>>.Ok(shippersDto);
        }
    }
}
