using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesByCategoryIdQueryHandler(ILogger<GetDishesForRestaurantQuery> logger,
    IRestaurantsRepository restaurantsRepository,
    IMapper mapper)
    : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishesDto>>
{
    public async Task<IEnumerable<DishesDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get all dishes for restaurant with id {RestaurantId}.", request.RestaurantId);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var result = mapper.Map<IEnumerable<DishesDto>>(restaurant.Dishes);

        return result;
    }
}
