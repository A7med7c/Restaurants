using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.MenuCategories.Commands.CreateMenuForRestaurant;
using Restaurants.Application.MenuCategories.Dtos;
using Restaurants.Application.MenuCategories.Queries.GetMenuCategoriesForRestaurant;
using Restaurants.Application.MenuCategories.Queries.GetMenuCategoryForRestaurant;

namespace Restaurants.Api.Controllers
{
    [Route("api/restaurant/{RestaurantId}/menuCategories")]
    [ApiController]
    public class MenuCategoriesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuCategoriesDto>>> GetAllMenuCategoriesForRestaurant([FromRoute]int restaurantId)
        {
            var menuCategories = await mediator.Send(new GetMenuCategoriesForRestaurantQuery(restaurantId));
            return Ok(menuCategories);
        }

        [HttpGet("{menuCategoryId}")]
        public async Task<ActionResult<MenuCategoriesDto>> GetMenuCategoryForRestaurant([FromRoute] int restaurantId, [FromRoute]int menuCategoryId)
        {
            var menuCategory = await mediator.Send(new GetMenuCategoryForRestaurantQuery(restaurantId, menuCategoryId));
            return Ok(menuCategory);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuCategoryForRestaurant([FromRoute] int restaurantId, [FromBody] CreateMenuForRestaurantCommand command)
        {
            command.RestaurantId = restaurantId;

            var menuCategoryId = await mediator.Send(command);
           
            return CreatedAtAction(nameof(GetMenuCategoryForRestaurant), new { restaurantId, menuCategoryId },null);
        }
    }
}
