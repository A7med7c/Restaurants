using MediatR;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.MenuCategories.Commands.CreateMenuForRestaurant;

public class CreateMenuForRestaurantCommand() : IRequest<int>
{
    public string Name { get; set; } = default!;
    public int RestaurantId { get; set;}
}
