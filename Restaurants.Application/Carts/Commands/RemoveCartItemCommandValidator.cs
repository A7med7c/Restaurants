using FluentValidation;

namespace Restaurants.Application.Carts.Commands;

public class RemoveCartItemCommandValidator : AbstractValidator<RemoveCartItemCommand>
{
    public RemoveCartItemCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0);
    }
}
