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
    IMenuCategoriesRepository menuCategoriesRepository) 
    : IRequestHandler<DeleteMenuCategoryForRestaurantCommand>
{
    public async Task Handle(DeleteMenuCategoryForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning("Delete menu category  with Id: {Id}", request.MenuCategoryId);

        await menuCategoriesRepository.DeleteAsync(request.MenuCategoryId);

    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       