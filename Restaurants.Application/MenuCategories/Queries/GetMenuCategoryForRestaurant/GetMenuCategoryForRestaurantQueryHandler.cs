using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.MenuCategories.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.MenuCategories.Queries.GetMenuCategoryForRestaurant;

public class GetMenuCategoryForRestaurantQueryHandler(ILogger<GetMenuCategoryForRestaurantQueryHandler> logger,
    IMenuCategoriesRepository menuCategoriesRepository,
    IMapper mapper) : IRequestHandler<GetMenuCategoryForRestaurantQuery, MenuCategoriesDto>
{
    public async Task<MenuCategoriesDto> Handle(GetMenuCategoryForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get menu category wit id : {Id} for restaurant  with id {RestaurantId}", request.MenuCategoryId, request.RestaurantId);

        var menuCategory = await menuCategoriesRepository.GetByIdAsync(request.MenuCategoryId)
            ?? throw new NotFoundException(nameof(MenuCategory), request.MenuCategoryId.ToString());

        var result =  mapper.Map<MenuCategoriesDto>(menuCategory);

        return result;
    }
}
