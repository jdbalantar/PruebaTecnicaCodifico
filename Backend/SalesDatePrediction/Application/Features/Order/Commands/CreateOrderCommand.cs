using Application.DTOs;
using Application.Interfaces.Infrastructure.Repositories;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Order.Commands
{
    public class CreateOrderCommand : IRequest<Result<int>>
    {

        [Required(ErrorMessage = "Customer ID is required.")]
        public int? Custid { get; set; }

        [Required(ErrorMessage = "Employee ID is required.")]
        public int Empid { get; set; }

        [Required(ErrorMessage = "Order date is required.")]
        public DateTime Orderdate { get; set; }

        [Required(ErrorMessage = "Required date is required.")]
        public DateTime Requireddate { get; set; }

        public DateTime? Shippeddate { get; set; }

        [Required(ErrorMessage = "Shipper ID is required.")]
        public int Shipperid { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Freight must be a non-negative value.")]
        public decimal Freight { get; set; }

        [Required(ErrorMessage = "Ship name is required.")]
        [StringLength(40, ErrorMessage = "Ship name cannot exceed 40 characters.")]
        public string Shipname { get; set; } = null!;

        [Required(ErrorMessage = "Ship address is required.")]
        [StringLength(60, ErrorMessage = "Ship address cannot exceed 60 characters.")]
        public string Shipaddress { get; set; } = null!;

        [Required(ErrorMessage = "Ship city is required.")]
        [StringLength(15, ErrorMessage = "Ship city cannot exceed 15 characters.")]
        public string Shipcity { get; set; } = null!;

        [StringLength(15, ErrorMessage = "Ship region cannot exceed 15 characters.")]
        public string? Shipregion { get; set; }

        [StringLength(10, ErrorMessage = "Ship postal code cannot exceed 10 characters.")]
        public string? Shippostalcode { get; set; }

        [Required(ErrorMessage = "Ship country is required.")]
        [StringLength(15, ErrorMessage = "Ship country cannot exceed 15 characters.")]
        public string Shipcountry { get; set; } = null!;
    }

    public class CreateOrderCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand, Result<int>>
    {

        public async Task<Result<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            bool exists = await unitOfWork.CustomerRepository.Exists(x => x.Custid == request.Custid, cancellationToken);
            if (!exists)
            {
                return Result<int>.NotFound($"Customer with ID {request.Custid} does not exist.");
            }

            exists = await unitOfWork.EmployeeRepository.Exists(x => x.Empid == request.Empid, cancellationToken);
            if (!exists)
            {
                return Result<int>.NotFound($"Employee with ID {request.Empid} does not exist.");
            }

            exists = await unitOfWork.ShipperRepository.Exists(x => x.Shipperid == request.Shipperid, cancellationToken);

            if (!exists)
            {
                return Result<int>.NotFound($"Shipper with ID {request.Shipperid} does not exist.");
            }



            var order = new Domain.Entities.Order()
            {
                Custid = request.Custid,
                Empid = request.Empid,
                Shipperid = request.Shipperid,
                Freight = request.Freight,
                Orderdate = request.Orderdate,
                Requireddate = request.Requireddate,
                Shipaddress = request.Shipaddress,
                Shipcity = request.Shipcity,
                Shipcountry = request.Shipcountry,
                Shipname = request.Shipname,
                Shippostalcode = request.Shippostalcode,
                Shipregion = request.Shipregion,
                Shippeddate = request.Shippeddate,
            };

            await unitOfWork.OrderRepository.AddAsync(order, cancellationToken: cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<int>.Created("Order created successfully", order.Orderid);

        }
    }

}
