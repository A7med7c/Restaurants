using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger,
    IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
        MinimumAgeRequirement requirement)
    {
        var currentyUser = userContext.GetCurrentUser();
        logger.LogInformation("User: {Email}, date of birth{DoB} - Handling MinimumAgeRequirement",
            currentyUser.Email, currentyUser.DateOfBirth);

        if (currentyUser.DateOfBirth == null)
        {
            logger.LogWarning("User date of birth is null");
            context.Fail();
            return Task.CompletedTask;
        } 

        if(currentyUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Authorization succeded");
            context.Succeed(requirement);   
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
