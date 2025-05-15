using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQueryHandler(
    ILogger<GetDishByIdForRestaurantQueryHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper) : IRequestHandler<GetDishByIdForRestaurantQuery, DishesDto>
{
    public async Task<DishesDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Retrieving dish with Id: {DishId}, for restaurant with id: {RestaurantId}",
            request.DishId, request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId)
            ?? throw new NotFoundException(nameof(Dish), request.DishId.ToString());

        var result = mapper.Map<DishesDto>(dish);
        return result;
    }
}