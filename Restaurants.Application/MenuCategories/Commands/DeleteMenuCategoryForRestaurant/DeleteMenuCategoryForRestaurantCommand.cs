using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Restaurants.Application.MenuCategories.Commands.DeleteMenuCategoryForRestaurant;

public class DeleteMenuCategoryForRestaurantCommand(int menuCategoryId) : IRequest
{
    public int MenuCategoryId { get; } = menuCategoryId;
}
