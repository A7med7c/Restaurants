using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

public class GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQuery> logger,
    IDishesRepository dishesRepository ,
    IMapper mapper) : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishesDto>>
{
    public async Task<IEnumerable<DishesDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get all dishes.");

        var dishes = await dishesRepository.GetAllAsync();

        var dishesDto = mapper.Map<IEnumerable<DishesDto>>(dishes);

        return dishesDto;
    }
}
