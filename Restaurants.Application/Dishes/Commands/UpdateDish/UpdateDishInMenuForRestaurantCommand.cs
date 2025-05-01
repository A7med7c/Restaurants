using MediatR;

namespace Restaurants.Application.Dishes.Commands.UpdateDish;
public class UpdateDishInMenuForRestaurantCommand(int restaurantId, int menuCategoryId, int dishId) : IRequest
{
    public int Id { get; } = dishId;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? KiloCalories { get; set; }
    public int RestaurantId { get; } = restaurantId;
    public int MenuCategoryId { get; } = menuCategoryId;
}