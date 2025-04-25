using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDisheByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

namespace Restaurants.Api.Controllers
{
    [Route("api/restaurant/{RestaurantId}/dishes")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishesDto>>> GetAllDishesForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }
        
        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishesDto>> GetDisheByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {

            var dish = await mediator.Send(new GetDisheByIdForRestaurantQuery(restaurantId,dishId));
            return Ok(dish);
        } 
        
        [HttpPost]
        public async Task<IActionResult> CreateDishFroRestaurant([FromRoute] int restaurantId, [FromBody]CreatDishCommand command)
        {
            command.RestaurantId = restaurantId;

            var dishId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetDisheByIdForRestaurant), new { restaurantId, dishId }, null);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDishesFroRestaurant([FromRoute]int restaurantId)
        {
            await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
            return NoContent();
        }
    }
}
