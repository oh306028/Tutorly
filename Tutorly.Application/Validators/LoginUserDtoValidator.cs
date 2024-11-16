using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos.CreateDtos;

namespace Tutorly.Application.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator()
        {
            RuleFor(e => e.Email).NotEmpty();
            RuleFor(p => p.Password).NotEmpty();
        }
    }
}
