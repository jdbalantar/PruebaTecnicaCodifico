using Application.DTOs;
using Application.DTOs.Employee;
using Application.Features.Employee.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    public class EmployeesController : BaseApiController<EmployeesController>
    {
        [HttpGet]
        [ProducesResponseType(typeof(Result<ICollection<EmployeeDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await Mediator.Send(new GetAllEmployeesQuery());
            return StatusCode(result.StatusCode, result);
        }
    }
}
