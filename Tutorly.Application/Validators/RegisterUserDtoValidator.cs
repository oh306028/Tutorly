using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {   
        public RegisterUserDtoValidator(IRepository<User> userRepository)
        {      

            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(30);

            RuleFor(p => p.Password)
                .NotEmpty()
                .MinimumLength(3);

            RuleFor(cp => cp.ConfirmPassword)
                .NotEmpty()
                .Equal(p => p.Password);

            RuleFor(fn => fn.FirstName)
                .NotEmpty()
                .MaximumLength(15);

            RuleFor(ln => ln.LastName)
                .NotEmpty()
                .MaximumLength(20);

            RuleFor(r => r.Role)
                .NotEmpty();

            RuleFor(e => e.Email).Custom((email, context) => {
                var emailInUse = userRepository.GetBy(e => e.Email == email);

                if(!(emailInUse is null))
                {
                    context.AddFailure("Email is already in use");
                }
            }); 
            
        }
    }
}
