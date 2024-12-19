using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos.Params;

namespace Tutorly.Application.Validators
{
    public class AcceptStudentValidator : AbstractValidator<AcceptStudentParams>
    {
        public AcceptStudentValidator()
        {
            RuleFor(s => s.StudentId)
                .NotEmpty();

            RuleFor(a => a.isAccepted)
                .NotEmpty();
        }
    }
}
