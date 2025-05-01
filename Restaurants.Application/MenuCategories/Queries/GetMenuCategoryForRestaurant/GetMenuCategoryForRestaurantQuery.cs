using MediatR;
using Restaurants.Application.MenuCategories.Dtos;

namespace Restaurants.Application.MenuCategories.Queries.GetMenuCategoryForRestaurant;

public class GetMenuCategoryForRestaurantQuery(int RestaurantId, int MenuCategoryId) : IRequest<MenuCategoriesDto>
{
    public int RestaurantId { get; } = RestaurantId;
    public int MenuCategoryId { get; } = MenuCategoryId;
}
