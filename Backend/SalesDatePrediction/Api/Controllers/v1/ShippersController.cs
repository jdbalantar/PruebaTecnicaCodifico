using Application.DTOs;
using Application.DTOs.Shipper;
using Application.Features.Shipper.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    public class ShippersController : BaseApiController<ShippersController>
    {
        [HttpGet]
        [ProducesResponseType(typeof(Result<ICollection<ShipperDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllShippers()
        {
            return Ok(await Mediator.Send(new GetAllShippersQuery()));
        }
    }
}
