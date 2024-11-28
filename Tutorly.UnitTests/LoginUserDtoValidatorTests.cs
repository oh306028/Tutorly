using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Validators;

namespace Tutorly.UnitTests
{
    public class LoginUserDtoValidatorTests
    {
        public static IEnumerable<object[]> GetValidLoginExampleData()
        {
            var list = new List<LoginUserDto>() { 
            
                new LoginUserDto()
                {
                    Email = "test@mail",
                    Password = "password1"
                }
            
            };

            return list.Select(s => new object[] { s });
        }


        [Theory]
        [MemberData(nameof(GetValidLoginExampleData))]
        public void LoginUserDto_ValidData_NoErrors(LoginUserDto model)
        {
            var validator = new LoginUserDtoValidator();
            var response = validator.TestValidate(model);

            response.ShouldNotHaveAnyValidationErrors();

        }
        
    }
}
