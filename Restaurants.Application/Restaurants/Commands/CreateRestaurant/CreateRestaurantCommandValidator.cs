using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;
public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validcategory = ["Italian", "Mexican", "Japanese", "American", "Indian"];
    public CreateRestaurantCommandValidator()
    {   
           
            RuleFor(x => x.Name)
           .Length(3, 100)
           .NotEmpty().WithMessage("Restaurant name is required.");
           
            RuleFor(x => x.Description)
                 .NotEmpty().WithMessage("Description is required.");

              RuleFor(x => x.Category)
            .Must(validcategory.Contains)
             .WithMessage("Invalid category. Please choose from: " + string.Join(", ", validcategory));

        RuleFor(x => x.ContactEmail)
                .EmailAddress().WithMessage("Please provide a valid email address");

            RuleFor(x => x.ContactNumber)
                .Matches(@"^\d{11}$")
                .WithMessage("Please provide a valid phone number.");

        RuleFor(x => x.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provide valid postal code (XX-XXX).");
}

}
