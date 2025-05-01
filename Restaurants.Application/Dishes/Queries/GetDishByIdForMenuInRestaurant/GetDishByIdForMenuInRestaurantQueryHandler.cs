using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForMenuInRestaurant;

public class GetDishByIdForMenuInRestaurantQueryHandler(ILogger<GetDishByIdForMenuInRestaurantQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository, IMapper mapper)
    : IRequestHandler<GetDishByIdForMenuInRestaurantQuery, DishesDto>
{
    async Task<DishesDto> IRequestHandler<GetDishByIdForMenuInRestaurantQuery, DishesDto>.Handle(GetDishByIdForMenuInRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get dish with id: {DishId} for menu category with id {MenuCategoryId}: in restaurant with id {RestaurantId}",
                request.DishId, request.MenuCategoryId, request.RestaurantId);


        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var menuCategory = restaurant.MenuCategories.FirstOrDefault(mc => mc.Id == request.MenuCategoryId)
            ?? throw new NotFoundException(nameof(MenuCategory), request.MenuCategoryId.ToString());

        var dish = menuCategory.Dishes.FirstOrDefault(d => d.Id == request.DishId)
            ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());

        var result = mapper.Map<DishesDto>(dish);
        return result;
    }
}
