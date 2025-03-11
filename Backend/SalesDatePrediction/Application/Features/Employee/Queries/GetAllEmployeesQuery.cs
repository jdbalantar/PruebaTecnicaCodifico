using Application.DTOs;
using Application.DTOs.Employee;
using Application.Interfaces.Infrastructure.Repositories;
using MediatR;

namespace Application.Features.Employee.Queries
{
    public class GetAllEmployeesQuery : IRequest<Result<ICollection<EmployeeDto>>> { }

    public class GetAllEmployeesQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllEmployeesQuery, Result<ICollection<EmployeeDto>>>
    {
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<Result<ICollection<EmployeeDto>>> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await unitOfWork.EmployeeRepository.GetAllAsync(cancellationToken);
            var employeeDtos = employees.Select(e => new EmployeeDto
            {
                Address = e.Address,
                Birthdate = e.Birthdate,
                City = e.City,
                Country = e.Country,
                Empid = e.Empid,
                Firstname = e.Firstname,
                Lastname = e.Lastname,
                Hiredate = e.Hiredate,
                Mgrid = e.Mgrid,
                Phone = e.Phone,
                Postalcode = e.Postalcode,
                Region = e.Region,
                Title = e.Title,
                Titleofcourtesy = e.Titleofcourtesy
            }).ToList();
            return Result<ICollection<EmployeeDto>>.Ok(employeeDtos);
        }
    }
}
