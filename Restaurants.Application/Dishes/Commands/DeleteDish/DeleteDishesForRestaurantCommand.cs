using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishesForRestaurantCommand(int dishId,int restaurantId) : IRequest
{
    public int Id { get; } = dishId;
    public int RestaurantId { get; } = restaurantId;
}
