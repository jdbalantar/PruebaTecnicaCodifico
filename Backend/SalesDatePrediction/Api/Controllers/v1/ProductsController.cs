using Application.DTOs;
using Application.DTOs.Product;
using Application.Features.Product.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.v1
{
    public class ProductsController : BaseApiController<ProductsController>
    {
        [HttpGet]
        [ProducesResponseType(typeof(Result<ICollection<ProductDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery()));
        }
    }
}
