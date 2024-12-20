using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Dtos.Params;

namespace Tutorly.Application.Validators
{
    public class DecideStudentApplicationValidator : AbstractValidator<DecideStudentApplicationDto>     
    {
        public DecideStudentApplicationValidator()  
        {
            RuleFor(s => s.StudentId)
                .NotEmpty();
            RuleFor(i => i.IsAccepted)
                .NotNull();

        }
    }
}
