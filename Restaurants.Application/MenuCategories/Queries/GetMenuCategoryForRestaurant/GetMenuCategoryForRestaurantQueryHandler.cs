using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.MenuCategories.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.MenuCategories.Queries.GetMenuCategoryForRestaurant;

public class GetMenuCategoryForRestaurantQueryHandler(ILogger<GetMenuCategoryForRestaurantQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper) : IRequestHandler<GetMenuCategoryForRestaurantQuery, MenuCategoriesDto>
{
    public async Task<MenuCategoriesDto> Handle(GetMenuCategoryForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get menu category wit id : {Id} for restaurant  with id {RestaurantId}", request.MenuCategoryId, request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var menuCategory = restaurant.MenuCategories.FirstOrDefault(mc => mc.Id == request.MenuCategoryId);

        var result =  mapper.Map<MenuCategoriesDto>(menuCategory);

        return result;
    }
}
