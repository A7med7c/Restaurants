using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForMenuInRestaurant;

public class GetDishByIdForMenuInRestaurantQuery(int restaurantId,int dishId, int MenuCategoryId) : IRequest<DishesDto>
{
    public int RestaurantId { get; } = restaurantId;
    public int DishId { get; } = dishId;
    public int MenuCategoryId { get; } = MenuCategoryId;
}
