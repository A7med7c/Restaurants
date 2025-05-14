using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.MenuCategories.Commands.CreateMenuCategoryForRestaurant;
using Restaurants.Application.MenuCategories.Commands.DeleteMenuCategoryForRestaurant;
using Restaurants.Application.MenuCategories.Dtos;
using Restaurants.Application.MenuCategories.Queries.GetMenuCategoriesForRestaurant;
using Restaurants.Application.MenuCategories.Queries.GetMenuCategoryForRestaurant;

namespace Restaurants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MenuCategoriesController(IMediator mediator) : ControllerBase
    {
        [HttpGet("by-restaurant/{restaurantId}")]
        public async Task<ActionResult<IEnumerable<MenuCategoriesDto>>> GetAllMenuCategoriesForRestaurant([FromRoute] int restaurantId)
        {
            var menuCategories = await mediator.Send(new GetMenuCategoriesForRestaurantQuery(restaurantId));
            return Ok(menuCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuCategoriesDto>> GetMenuCategoryForRestaurant([FromRoute] int restaurantId, [FromRoute] int id)
        {
            var menuCategory = await mediator.Send(new GetMenuCategoryForRestaurantQuery(restaurantId, id));
            return Ok(menuCategory);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuCategoryForRestaurant( [FromBody] CreateMenuCategoryForRestaurantCommand command)
        {
            var menuCategoryId = await mediator.Send(command);

            return CreatedAtAction(nameof(GetMenuCategoryForRestaurant),
                new { id = menuCategoryId }, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMenuCategoryForRestaurant([FromRoute] int id)
        {
            await mediator.Send(new DeleteMenuCategoryForRestaurantCommand(id));

            return NoContent();
        }
    }
}
