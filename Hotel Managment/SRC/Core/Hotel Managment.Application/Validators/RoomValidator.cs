using FluentValidation;
using Hotel_Managment.Domain.Entities;


namespace Hotel_Managment.Application.Validators
{
    public class RoomValidator:AbstractValidator<Room>
    {
        public RoomValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("NAme is important")
               .MaximumLength(100).WithMessage("Name may consist maximum 100 characters")
               .MinimumLength(3).WithMessage("Name must consist minimum 3 characters"); 
            RuleFor(x => x.Description)
               .NotEmpty().WithMessage("Description is important")
               .MaximumLength(100).WithMessage("Description may consist maximum 100 characters")
               .MinimumLength(5).WithMessage("Description must consist minimum 5 characters");
            RuleFor(x => x.RoomImages).NotEmpty().WithMessage("The picture must be selected");
            RuleFor(x => x.Price).Must(x => x >= 10 && x <= 999999.99m);
            RuleFor(x => x.Size).Must(x => x >= 10 && x <= 999999.99m);
            RuleFor(x => x.Bed).Must(x => x >= 1 && x <= 999999.99m);
            RuleFor(x => x.Capacity).Must(x => x >= 10 && x <= 999999.99m);

            RuleFor(x => x.CategoryId).Must(x => x > 0);
        }
    }
}
