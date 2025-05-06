using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements.OwnsTwoRestaurants;

public class OwnsTwoRestaurantsReqirement(int ownedRestaurants) : IAuthorizationRequirement
{
    public int OwnedRestaurants { get; } = ownedRestaurants;
}
