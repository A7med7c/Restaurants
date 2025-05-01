using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishInMenuForRestaurantCommandHandler(
    ILogger<CreateDishInMenuForRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IMapper mapper)
    : IRequestHandler<CreateDishInMenuForRestaurantCommand, int>
{
    public async Task<int> Handle(CreateDishInMenuForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@DishRequest}", request);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var menuCategory = restaurant.MenuCategories
            .FirstOrDefault(mc => mc.Id == request.MenuCategoryId)
            ?? throw new NotFoundException(nameof(MenuCategory), request.MenuCategoryId.ToString());

        var dish = mapper.Map<Dish>(request);

        return await dishesRepository.CreateAsync(dish);
    }
}
