using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Orders.Commands;

public class UpdateOrderStatusCommandHandler(
    ILogger<UpdateOrderStatusCommandHandler> logger,
    IOrdersRepository ordersRepository) : IRequestHandler<UpdateOrderStatusCommand>
{
    public async Task Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update order status for id: {OrderId} to {OrderStatus}", request.Id, request.Status);

        var order = await ordersRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundException(nameof(Order), request.Id.ToString());

        order.UpdateStatus(request.Status);
        await ordersRepository.SaveChanges();
    }
}
