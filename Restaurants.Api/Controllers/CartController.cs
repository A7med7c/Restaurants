using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Carts.Commands;
using Restaurants.Application.Carts.Dtos;
using Restaurants.Application.Carts.Queries;

namespace Restaurants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<CartDto>> GetCurrentCart()
        {
            var cart = await mediator.Send(new GetCartQuery());
            return Ok(cart);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddItem([FromBody] AddToCartCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("items/{id}")]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] UpdateCartItemCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("items/{id}")]
        public async Task<IActionResult> RemoveItem([FromRoute] int id)
        {
            await mediator.Send(new RemoveCartItemCommand { Id = id });
            return NoContent();
        }
    }
}
