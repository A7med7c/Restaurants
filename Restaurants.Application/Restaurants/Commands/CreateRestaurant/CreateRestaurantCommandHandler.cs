using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;
public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> logger,
    IMapper mapper , IRestaurantsRepository restaurantsRepository) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        //@ => make serilog print details of request
        logger.LogInformation("Creating a new Restaurant{@Restaurant}", request);
        var restaurant = mapper.Map<Restaurant>(request);

        int id = await restaurantsRepository.CreateAsync(restaurant);
        return id;

    }
}
