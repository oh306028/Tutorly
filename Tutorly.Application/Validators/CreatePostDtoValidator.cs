using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos.CreateDtos;

namespace Tutorly.Application.Validators
{
    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(c => c.CategoryId)
                .NotEmpty();

            RuleFor(s => s.MaxStudentAmount)
                .NotEmpty()
                .GreaterThan(0)
                .LessThan(20);

            RuleFor(r => r.IsRemotely)
                .NotNull();

            RuleFor(p => p.IsAtStudentPlace)
                .NotNull();

            
            RuleFor(g => g.StudentsGrade)
                .NotEmpty();

            RuleFor(t => t.HappensAt)
                .NotEmpty();

        }
    }
}
