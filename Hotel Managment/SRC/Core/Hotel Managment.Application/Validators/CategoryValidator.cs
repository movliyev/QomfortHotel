using FluentValidation;
using Hotel_Managment.Domain.Entities;


namespace Hotel_Managment.Application.Validators
{
    public class CategoryValidator:AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("The name of the category cannot be left blank")
                .MaximumLength(50).WithMessage("No more than 50 letters")
                .MinimumLength(3).WithMessage("It should consist of at least 3 letters")
                .Matches(@"^[a-zA-Z\s]*$");

        }
    }
}
