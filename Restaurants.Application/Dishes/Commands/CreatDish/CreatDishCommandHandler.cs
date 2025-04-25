
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreatDish;

public class CreatDishCommandHandler(ILogger<CreatDishCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,IDishesRepository dishesRepository,
    IMapper mapper) : IRequestHandler<CreatDishCommand, int>
{
    public async Task<int> Handle(CreatDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@DishRequest}", request);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant == null)
            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = mapper.Map<Dish>(request);

        return await dishesRepository.CreateAsync(dish);
    }
}
