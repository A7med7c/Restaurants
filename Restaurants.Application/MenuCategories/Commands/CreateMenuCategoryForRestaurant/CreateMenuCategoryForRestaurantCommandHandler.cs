using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.MenuCategories.Commands.CreateMenuCategoryForRestaurant;

public class CreateMenuCategoryForRestaurantCommandHandler(ILogger<CreateMenuCategoryForRestaurantCommandHandler> logger,
    IMenuCategoriesRepository menuCategoriesRepository,
    IMapper mapper) : IRequestHandler<CreateMenuCategoryForRestaurantCommand, int>
{
    public async Task<int> Handle(CreateMenuCategoryForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new menu category {@Request}", request);

        var menuCategory = mapper.Map<MenuCategory>(request);

        return await menuCategoriesRepository.AddAsync(menuCategory);

    }
}
