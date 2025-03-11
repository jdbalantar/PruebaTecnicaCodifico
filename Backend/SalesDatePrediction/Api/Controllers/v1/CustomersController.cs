using Application.DTOs;
using Application.DTOs.Customer;
using Application.Features.Customer.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    public class CustomersController : BaseApiController<CustomersController>
    {
        [HttpGet]
        [ProducesResponseType(typeof(Result<ICollection<CustomerWithOrderHistoryDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomersWithOrderHistory()
        {
            return Ok(await Mediator.Send(new GetCustomersWithOrderHistoryQuery()));
        }
    }
}
