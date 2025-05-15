using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQuery(int RestaurantId) : IRequest<IEnumerable<DishesDto>>
{
    public int RestaurantId { get; } = RestaurantId;
}
