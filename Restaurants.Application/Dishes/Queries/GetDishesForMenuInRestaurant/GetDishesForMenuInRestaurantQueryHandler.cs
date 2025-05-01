using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForMenuInRestaurantQueryHandler(ILogger<GetDishesForMenuInRestaurantQuery> logger,
    IRestaurantsRepository restaurantsRepository, IMapper mapper)
    : IRequestHandler<GetDishesForMenuInRestaurantQuery, IEnumerable<DishesDto>>
{
    public async Task<IEnumerable<DishesDto>> Handle(GetDishesForMenuInRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get all dishes for menu category with id: {Id}, restaurant with id {RestaurantId}.", request.MenuCategoryId,request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var menuCategory = restaurant.MenuCategories.FirstOrDefault(mc => mc.Id == request.MenuCategoryId)
          ?? throw new NotFoundException(nameof(MenuCategory), request.MenuCategoryId.ToString());

        var dishes = mapper.Map<IEnumerable<DishesDto>>(menuCategory.Dishes);

        return dishes;
    }
}
