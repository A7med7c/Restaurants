using MediatR;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Commands.UpdateDish;
public class UpdateForRestaurantCommand(int restaurantId, int dishId) : IRequest
{
    public int Id { get; } = dishId;
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? KiloCalories { get; set; }
    public int RestaurantId { get; } = restaurantId;
}