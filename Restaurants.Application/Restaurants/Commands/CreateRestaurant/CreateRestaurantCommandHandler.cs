using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;
public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
    IMapper mapper,
    IRestaurantsRepository restaurantsRepository,
    IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser =  userContext.GetCurrentUser();

        //@ => make serilog print details of request
        logger.LogInformation("{UserEmail} [{UserId}] Creating a new Restaurant{@Restaurant}",
            currentUser!.Email, currentUser.Id, request);

        var restaurant = mapper.Map<Restaurant>(request);
        restaurant.OwnerId = currentUser.Id;

        int id = await restaurantsRepository.CreateAsync(restaurant);
        return id;

    }
}
