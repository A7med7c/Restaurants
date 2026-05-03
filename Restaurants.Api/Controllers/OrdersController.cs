using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Orders.Commands;
using Restaurants.Application.Orders.Dtos;
using Restaurants.Application.Orders.Queries;

namespace Restaurants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<OrdersDto>> GetOrderById(int id)
        {
            var order = await mediator.Send(new GetOrderByIdQuery(id));
            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetOrderById), new { id }, null);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutResultDto>> Checkout([FromBody] CheckoutCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateOrderStatus(int id, [FromBody] UpdateOrderStatusCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

    }
}
