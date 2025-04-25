using FluentValidation;

namespace Restaurants.Application.Dishes.Commands;

public class CreatDishCommandValidator : AbstractValidator<CreatDishCommand>
{
    public CreatDishCommandValidator()
    {
        RuleFor(d => d.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price shouldn't be nwgative");

        RuleFor(d => d.KiloCalories)
            .GreaterThanOrEqualTo(0)
            .WithMessage("KiloCalories shouldn't be nwgative");
           
    }
}
