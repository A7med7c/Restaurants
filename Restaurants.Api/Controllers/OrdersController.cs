using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Orders.Commands;

namespace Restaurants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        [HttpPost] 
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            int id = await mediator.Send(command);
            return Ok(id);
        }
    }
}
