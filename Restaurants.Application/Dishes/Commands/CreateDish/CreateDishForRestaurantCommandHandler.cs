using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishForRestaurantCommandHandler(
    ILogger<CreateDishForRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IRestauratntAuthorizationServices restauratntAuthorizationServices,
    IMapper mapper)
    : IRequestHandler<CreateDishForRestaurantCommand, int>
{
    public async Task<int> Handle(CreateDishForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@DishRequest}", request);
        
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
        if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        if (!restauratntAuthorizationServices.IsAuthorize(ResourceOperation.Create, restaurant))
            throw new ForbiddenException();

        var dish = mapper.Map<Dish>(request);

        return await dishesRepository.AddAsync(dish);
    }
}
