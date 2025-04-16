using MediatR;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GatAllRestaurants;
public class GetAllRestaurantsQuery : IRequest<IEnumerable<RestaurantDto>>
{

}
