using FluentValidation;
using Hotel_Managment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Managment.Application.Validators
{
    public class RegisterValidator:AbstractValidator<AppUser>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty()
                .Matches(@"^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$")
                .MaximumLength(256).WithMessage("Email may consist maximum 100 characters");
           
            RuleFor(x => x.UserName).NotEmpty()
                .MaximumLength(256).WithMessage("UserName may consist maximum 256 characters")
                .MinimumLength(4).WithMessage("UserName must consist minimum 4 characters");
            RuleFor(x => x.Name).NotEmpty()
                .Matches(@"^[a-zA-Z\s]*$")
                .MaximumLength(50).WithMessage("Name may consist maximum 50 characters")
                .MinimumLength(3).WithMessage("Name must consist minimum 3 characters");
            RuleFor(x => x.Surname).NotEmpty()
                .Matches(@"^[a-zA-Z\s]*$")
                .MaximumLength(50).WithMessage("Name may consist maximum 50 characters")
                .MinimumLength(3).WithMessage("Name must consist minimum 3 characters");
           
        }
    }
}
