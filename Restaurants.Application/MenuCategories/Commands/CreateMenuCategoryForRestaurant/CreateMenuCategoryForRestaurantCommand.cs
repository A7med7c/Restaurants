using MediatR;

namespace Restaurants.Application.MenuCategories.Commands.CreateMenuCategoryForRestaurant;

public class CreateMenuCategoryForRestaurantCommand() : IRequest<int>
{
    public string Name { get; set; } = default!;
    public int RestaurantId { get; set;}
}
