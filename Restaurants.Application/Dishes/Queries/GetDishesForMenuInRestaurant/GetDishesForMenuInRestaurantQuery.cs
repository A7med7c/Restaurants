using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForMenuInRestaurantQuery(int RestaurantId,int MenuCategoryId) : IRequest<IEnumerable<DishesDto>>
{
    public int RestaurantId { get; } = RestaurantId;
    public int MenuCategoryId { get; } = MenuCategoryId;
}
