using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishInMenuForRestaurantCommand(int restaurantId,int MenuCategoryId, int DishId) : IRequest
{
    public int RestaurantId { get; } = restaurantId;
    public int MenuCategoryId { get; } = MenuCategoryId;
    public int DishId { get; } = DishId;
}
