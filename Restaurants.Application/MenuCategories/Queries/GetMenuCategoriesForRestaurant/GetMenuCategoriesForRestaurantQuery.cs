using MediatR;
using Restaurants.Application.MenuCategories.Dtos;

namespace Restaurants.Application.MenuCategories.Queries.GetMenuCategoriesForRestaurant;

public class GetMenuCategoriesForRestaurantQuery(int RestaurantId) : IRequest<IEnumerable<MenuCategoriesDto>>
{
    public int RestaurantId { get; } = RestaurantId;
}
