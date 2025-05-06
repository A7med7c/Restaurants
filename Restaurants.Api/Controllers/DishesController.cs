using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDish;
using Restaurants.Application.Dishes.Commands.UpdateDish;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishByIdForMenuInRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

namespace Restaurants.Api.Controllers
{
    [Route("api/restaurants/{RestaurantId}/menuCategories/{MenuCategoryId}/dishes")]
    [ApiController]
    [Authorize]
    public class DishesController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DishesDto>>> GetAllDishes([FromRoute] int restaurantId, [FromRoute] int menuCategoryId)
        {
            var dishes = await mediator.Send(new GetDishesForMenuInRestaurantQuery(restaurantId, menuCategoryId));
            return Ok(dishes);
        }
        
        [HttpGet("{dishId}")]
        public async Task<ActionResult<DishesDto>> GetDishById([FromRoute] int restaurantId, [FromRoute] int dishId,[FromRoute] int menuCategoryId)
        {

            var dish = await mediator.Send(new GetDishByIdForMenuInRestaurantQuery(restaurantId, dishId, menuCategoryId));
            return Ok(dish);
        } 
        
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId,[FromRoute] int menuCategoryId, [FromBody]CreateDishInMenuForRestaurantCommand command)
        {
            command.RestaurantId = restaurantId; 
            command.MenuCategoryId = menuCategoryId;

            var dishId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetDishById), new { restaurantId,menuCategoryId, dishId }, null);
        }

        [HttpPatch("{dishId}")]
        public async Task<IActionResult> UpdateDish(
            [FromRoute] int restaurantId, 
            [FromRoute] int menuCategoryId, 
            [FromRoute] int dishId,
            [FromBody]  DishesDto dishesDto)
        {
            // Create a new command instance with route parameters and copy body data
            var updatedCommand = new UpdateDishInMenuForRestaurantCommand(restaurantId, menuCategoryId, dishId)
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
        public async Task<IActionResult> DeleteDish(
            [FromRoute] int restaurantId,
            [FromRoute] int menuCategoryId,
            [FromRoute] int dishId)
        {
            await mediator.Send(new DeleteDishInMenuForRestaurantCommand(restaurantId, menuCategoryId, dishId));
            return NoContent();
        }

    }
}
