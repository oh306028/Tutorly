using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos.CreateDtos;

namespace Tutorly.Application.Validators
{
    public class CreateAddressDtoValidator : AbstractValidator<CreateAddressDto>
    {
        public CreateAddressDtoValidator()
        {
            RuleFor(c => c.City)
                .NotEmpty()
                .MaximumLength(15);

            RuleFor(s => s.Street)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(n => n.Number)
                .NotEmpty()
                .MaximumLength(10);
        }
    }
}
