using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantbyId;

namespace Restaurants.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
        {
            var resturaurants = await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(resturaurants);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantDto>> GetById([FromRoute]int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantbyIdQuery(id));
   
            return Ok(restaurant);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {
            int id = await mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult?> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
        {
            //var restaurant = GetById(id);
           //bind id 
            command.Id = id;
             await mediator.Send(command);

                return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
             await mediator.Send(new DeleteRestaurantCommand(id));

                return NoContent();
        }
      
    }
}
