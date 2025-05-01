using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.MenuCategories.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.MenuCategories.Queries.GetMenuCategoriesForRestaurant;

public class GetMenuCategoriesForRestaurantQueryHandler(ILogger<GetMenuCategoriesForRestaurantQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper) : IRequestHandler<GetMenuCategoriesForRestaurantQuery, IEnumerable<MenuCategoriesDto>>
{
    public async Task<IEnumerable<MenuCategoriesDto>> Handle(GetMenuCategoriesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get all Menu Categories for restaurant with id {RestaurantId}.", request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
        ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());


        var menuCategories = mapper.Map<IEnumerable<MenuCategoriesDto>>(restaurant.MenuCategories);

        return menuCategories; 
    }
}
