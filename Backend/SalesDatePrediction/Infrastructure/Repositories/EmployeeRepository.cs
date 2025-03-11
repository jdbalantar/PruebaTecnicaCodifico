using Application.Interfaces.Infrastructure.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EmployeeRepository(DbContext context) : BaseRepository<Employee>(context), IEmployeeRepository
    {
    }
}
