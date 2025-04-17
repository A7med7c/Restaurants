using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

namespace Restaurants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllDishes()
        {
            var dishes = await mediator.Send(new GetDishesForRestaurantQuery());
            
            return Ok(dishes);
        } 
    }
}
