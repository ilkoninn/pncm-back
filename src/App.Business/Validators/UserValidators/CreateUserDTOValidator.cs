using App.Core.DTOs.UserDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.Validators.UserValidators
{
    public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
    {
        public CreateUserDTOValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name is required.")
                .MaximumLength(100)
                .WithMessage("Full name must not exceed 100 characters.");
        }
    }
}
