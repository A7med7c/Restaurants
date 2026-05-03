using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Payments.Commands;

public class HandlePaymobCallbackCommandHandler(
    ILogger<HandlePaymobCallbackCommandHandler> logger,
    IOrdersRepository ordersRepository) : IRequestHandler<HandlePaymobCallbackCommand>
{
    public async Task Handle(HandlePaymobCallbackCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling Paymob callback for order {OrderId} with status {PaymentStatus}",
            request.OrderId, request.PaymentStatus);

        var order = await ordersRepository.GetByIdAsync(request.OrderId)
            ?? throw new NotFoundException(nameof(Order), request.OrderId.ToString());

        order.PaymentStatus = request.PaymentStatus;
        await ordersRepository.SaveChanges();
    }
}
