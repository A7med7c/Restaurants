using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;

namespace Restaurants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IRestaurantsService restaurantsService) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resturaurants = await restaurantsService.GetAllRestaurants();
            return Ok(resturaurants);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult?> GetById([FromRoute]int id)
        {
            var restaurant = await restaurantsService.GetRestaurant(id);
            if (restaurant is null)
                return NotFound();

            return Ok(restaurant);
        }
        
    }
}
