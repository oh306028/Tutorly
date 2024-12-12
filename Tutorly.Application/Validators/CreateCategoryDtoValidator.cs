using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CreateCategoryDto>
    {

        public CreateCategoryDtoValidator(IRepository<Category> categoryRepository)
        {


            RuleFor(n => n.Name)
                .NotEmpty()
                .MaximumLength(30);

            RuleFor(n => n.Name)
                .Custom((name, context) =>
                {
                    var createdCategory = categoryRepository.GetBy(n => n.Name == name);

                    if (createdCategory != null)
                        context.AddFailure("Category already exists");
                });

        }

    }
}
