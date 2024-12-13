using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos.CreateDtos;

namespace Tutorly.Application.Validators
{
    public class UpdateUserDtoValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserDtoValidator()
        {
            RuleFor(n => n.FirstName)
                .MaximumLength(15)
                .NotEmpty();
                

            RuleFor(n => n.LastName)
               .MaximumLength(20)
               .NotEmpty();

            RuleFor(n => n.Email)
             .MaximumLength(30)
             .EmailAddress()
             .NotEmpty();

        }
    }
}
