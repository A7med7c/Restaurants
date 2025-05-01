using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Dishes.Commands.UpdateDish;

public class UpdateDishInMenuForRestaurantCommandHandler(ILogger<UpdateDishInMenuForRestaurantCommandHandler> logger,
   IRestaurantsRepository restaurantsRepository,
   IMapper mapper,
   IDishesRepository dishesRepository) : IRequestHandler<UpdateDishInMenuForRestaurantCommand>
{
    public async Task Handle(UpdateDishInMenuForRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update dish with id : {DishId} with {@UpdatedDish}", request.Id, request);
        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId)
            ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var menuCategory = restaurant.MenuCategories
           .FirstOrDefault(mc => mc.Id == request.MenuCategoryId)
           ?? throw new NotFoundException(nameof(MenuCategory), request.MenuCategoryId.ToString());

        var dish = menuCategory.Dishes.FirstOrDefault(d => d.Id == request.Id)
           ?? throw new NotFoundException(nameof(Dish), request.Id.ToString());
        
        mapper.Map(request, dish);

        await dishesRepository.SaveChanges();
    }
}
