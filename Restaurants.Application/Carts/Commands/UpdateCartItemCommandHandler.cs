using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Carts.Commands;

public class UpdateCartItemCommandHandler(
    ILogger<UpdateCartItemCommandHandler> logger,
    IUserContext userContext,
    ICartRepository cartRepository) : IRequestHandler<UpdateCartItemCommand>
{
    public async Task Handle(UpdateCartItemCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("{UserEmail} [{UserId}] Updating cart item {CartItemId}",
            currentUser!.Email, currentUser.Id, request.Id);

        var cart = await cartRepository.GetByUserIdAsync(currentUser.Id)
            ?? throw new NotFoundException(nameof(Cart), currentUser.Id);

        var item = cart.Items.FirstOrDefault(i => i.Id == request.Id)
            ?? throw new NotFoundException(nameof(CartItem), request.Id.ToString());

        item.Quantity = request.Quantity;
        await cartRepository.SaveChanges();
    }
}
