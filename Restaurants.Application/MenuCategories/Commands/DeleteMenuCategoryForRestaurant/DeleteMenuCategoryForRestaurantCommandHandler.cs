using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.MenuCategories.Commands.DeleteMenuCategoryForRestaurant;

public class DeleteMenuCategoryForRestaurantCommandHandler(
    ILogger<DeleteMenuCategoryForRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMenuCategoriesRepository menuCategoriesRepository,
    IRestauratntAuthorizationServices restauratntAuthorizationServices) 
    : IRequestHandler<DeleteMenuCategoryForRestaurantCommand>
{
    public async Task Handle(DeleteMenuCategoryForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Delete menu category  with Id: {Id}", request.MenuCategoryId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
           ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        
        if (restauratntAuthorizationServices.IsAuthorize(ResourceOperation.Delete, restaurant))
            throw new ForbiddenException();

        var menuCategory = restaurant.MenuCategories
           .FirstOrDefault(mc => mc.Id == request.MenuCategoryId)
           ?? throw new NotFoundException(nameof(MenuCategory), request.MenuCategoryId.ToString());

        await menuCategoriesRepository.DeleteAsync(menuCategory.Id);

    }
}
