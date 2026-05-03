using MediatR;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Payments.Commands;

public class HandlePaymobCallbackCommand : IRequest
{
    public int OrderId { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}
