using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Commands.UpdateDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

namespace Restaurants.Api.Controllers
{
    [Route("api/restaurants/{restaurantId}/dishes")]
    [ApiController]
    //[Authorize]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishForRestaurantCommand command)
        {
            command.RestaurantId = restaurantId;

            var dishId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
        }

        [HttpGet]
       // [Authorize(Policy = PolicyNames.AtLeast20)]
        public async Task<ActionResult<IEnumerable<DishesDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishesDto>> GetByIdForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
            return Ok(dish);
        }
        
        [HttpPut("{dishId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromRoute] int restaurantId, [FromRoute] int dishId, [FromBody] DishesDto dishesDto)
        {
            var updatedCommand = new UpdateForRestaurantCommand(restaurantId, dishId)
            {
               Name = dishesDto.Name,
               Description = dishesDto.Description,
               Price = dishesDto.Price,
               KiloCalories = dishesDto.KiloCalories
            };
            await mediator.Send(updatedCommand);
            return NoContent();
        }


        [HttpDelete("{dishId}")]
        public async Task<IActionResult> DeleteDishForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            await mediator.Send(new DeleteDishesForRestaurantCommand(dishId,restaurantId));
            return NoContent();
        }
    }
}
