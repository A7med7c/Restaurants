using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;
using AutoMapper;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Dishes.Commands.DeleteDish;

public class DeleteDishesForRestaurantCommandHandler(
    ILogger<DeleteDishesForRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IRestauratntAuthorizationServices restauratntAuthorizationServices)
    : IRequestHandler<DeleteDishInMenuForRestaurantCommand>
{
    public async Task Handle(DeleteDishInMenuForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Delete dish with Id: {Id} in menu with Id: {menuId} for restaurant with id: {RestaurantId}"
            ,request.DishId, request.MenuCategoryId,request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ??throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (restauratntAuthorizationServices.IsAuthorize(ResourceOperation.Delete, restaurant))
            throw new ForbiddenException();

        var menuCategory = restaurant.MenuCategories
           .FirstOrDefault(mc => mc.Id == request.MenuCategoryId)
           ?? throw new NotFoundException(nameof(MenuCategory), request.MenuCategoryId.ToString());
        
        var dish = menuCategory.Dishes.FirstOrDefault(d => d.Id == request.DishId)
           ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());

          await dishesRepository.DeleteAsync(dish);
    }
}
