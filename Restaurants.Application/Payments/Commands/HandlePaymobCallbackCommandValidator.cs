using FluentValidation;

namespace Restaurants.Application.Payments.Commands;

public class HandlePaymobCallbackCommandValidator : AbstractValidator<HandlePaymobCallbackCommand>
{
    public HandlePaymobCallbackCommandValidator()
    {
        RuleFor(c => c.OrderId)
            .GreaterThan(0);

        RuleFor(c => c.PaymentStatus)
            .IsInEnum();
    }
}
