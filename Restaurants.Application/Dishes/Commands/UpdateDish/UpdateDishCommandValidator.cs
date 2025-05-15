using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.UpdateDish;

public class UpdateDishCommandValidator : AbstractValidator<UpdateForRestaurantCommand>
{
    public UpdateDishCommandValidator()
    {
        RuleFor(d => d.Price)
    .GreaterThanOrEqualTo(0)
    .WithMessage("Price shouldn't be nwgative");

        RuleFor(d => d.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("KiloCalories shouldn't be nwgative");
    }
}
