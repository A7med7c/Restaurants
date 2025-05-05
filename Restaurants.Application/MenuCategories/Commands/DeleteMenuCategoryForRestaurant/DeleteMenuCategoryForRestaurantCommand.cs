using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Restaurants.Application.MenuCategories.Commands.DeleteMenuCategoryForRestaurant;

public class DeleteMenuCategoryForRestaurantCommand(int restaurantId, int menuCategoryId) : IRequest
{
    public int RestaurantId { get; } = restaurantId;
    public int MenuCategoryId { get; } = menuCategoryId;
}
