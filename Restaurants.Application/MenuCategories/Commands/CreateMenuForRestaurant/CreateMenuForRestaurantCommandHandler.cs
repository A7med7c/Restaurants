using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.MenuCategories.Commands.CreateMenuForRestaurant;

public class CreateMenuForRestaurantCommandHandler(ILogger<CreateMenuForRestaurantCommandHandler> logger,
    IMenuCategoriesRepository menuCategoriesRepository,
    IMapper mapper) : IRequestHandler<CreateMenuForRestaurantCommand, int>
{
    public async Task<int> Handle(CreateMenuForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new menu category {@Request}", request);


        var menuCategory = mapper.Map<MenuCategory>(request);

        return await menuCategoriesRepository.CreateAsync(menuCategory);

    }
}
