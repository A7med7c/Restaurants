using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;

namespace Restaurants.Infrastructure.Authorization.Requirements.OwnsTwoRestaurants;

public class OwnsTwoRestaurantsReqirementHandler(
    ILogger<OwnsTwoRestaurantsReqirementHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IUserContext userContext) : AuthorizationHandler<OwnsTwoRestaurantsReqirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnsTwoRestaurantsReqirement requirement)
    {
        var user = userContext.GetCurrentUser();

        var resturants = await restaurantsRepository.GetAllAsync();

        var ownedRestaurants = resturants.Count(n => n.OwnerId == user!.Id);

        if(ownedRestaurants >= requirement.OwnedRestaurants)
             context.Succeed(requirement);
        else
            context.Fail();
    }
}
