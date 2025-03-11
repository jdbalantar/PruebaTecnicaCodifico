using Application.DTOs;
using Application.DTOs.Order;
using Application.Features.Order.Commands;
using Application.Features.Order.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    public class OrdersController : BaseApiController<OrdersController>
    {
        [HttpGet("getByCustomer/{customerId:int}")]
        [ProducesResponseType(typeof(Result<ICollection<OrderDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<ICollection<OrderDto>>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrdersByCustomer(int customerId)
        {
            var result = await Mediator.Send(new GetOrdersByCustomerQuery { CustomerId = customerId });
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<int>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return StatusCode(result.StatusCode, result);
        }

    }
}
