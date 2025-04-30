using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UpdateUserDetails;

public class UpdateUseDetailsCommandHandler(IUserContext userContext,
    ILogger<UpdateUseDetailsCommandHandler> logger,
    IUserStore<User> userStore) : IRequestHandler<UpdateUseDetailsCommand>
{
    // IUserStore == makeing IUserRepository
    public async Task Handle(UpdateUseDetailsCommand request, CancellationToken cancellationToken)
    {
        var user = userContext.GetCurrentUser();

        logger.LogInformation("Updating User: {userId},with {@Request}", user?.Id,request);

        var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(User), user.Id);
        
        dbUser.Nationality = request.Nationality;
        dbUser.DateOfBirth = request.DateOfBirth;

        await userStore.UpdateAsync(dbUser, cancellationToken);
    }
}
