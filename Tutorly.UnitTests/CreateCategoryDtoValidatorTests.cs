using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Dtos.DisplayDtos;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Validators;
using Tutorly.Domain.Models;
using Tutorly.Infrastructure.Repos;

namespace Tutorly.UnitTests
{
    public class CreateCategoryDtoValidatorTests
    {

        [Theory]
        [InlineData("Geography")]
        [InlineData("Phylosophy")]
        [InlineData("Physics")]

        public void CreateCategory_ValidData_NoExceptions(string name)
        {
            var newCategory = new CreateCategoryDto(name);
            var categoryRepoMock = new Mock<IRepository<Category>>(); 

            categoryRepoMock.Setup(s => s.GetBy(It.IsAny<Expression<Func<Category, bool>>>()))
      .Returns((Expression<Func<Category, bool>> predicate) => null); 


            var validator = new CreateCategoryDtoValidator(categoryRepoMock.Object);

            var result = validator.TestValidate(newCategory);

            result.ShouldNotHaveAnyValidationErrors();


        }


        [Fact]
        public void CreateCategory_AlreadyCreatedCategory_Fails()    
        {
            var newCategory = new CreateCategoryDto("Maths");
            var categoryRepoMock = new Mock<IRepository<Category>>();

            categoryRepoMock.Setup(s => s.GetBy(It.IsAny<Expression<Func<Category, bool>>>()))
      .Returns((Expression<Func<Category, bool>> predicate) => new Category() { Name= "Maths"});


            var validator = new CreateCategoryDtoValidator(categoryRepoMock.Object);

            var result = validator.TestValidate(newCategory);

            result.ShouldHaveAnyValidationError();


        }




    }
}
