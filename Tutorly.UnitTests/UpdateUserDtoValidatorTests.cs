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
    public class UpdateUserDtoValidatorTests
    {
        public static IEnumerable<object[]> GetTestData()
        {
            var list = new List<UpdateUserDto>()
            {
                new UpdateUserDto()
                {
                    FirstName = "John"
                },

                new UpdateUserDto()
                {
                    Email = "Test@gmail.com"
                },

                new UpdateUserDto()
                {
                    LastName = "Surname"
                },
                
                new UpdateUserDto()
                {

                }

            };

            return list.Select(e => new object[] { e });
        }

        public static IEnumerable<object[]> GetInvalidTestData()
        {
            var list = new List<UpdateUserDto>()
            {
                new UpdateUserDto()
                {
                    FirstName = "Too long first name so it cannot be patched"
                },

                new UpdateUserDto()
                {
                    Email = "email is not an email"
                },

                new UpdateUserDto()
                {
                    Email = "Toolongemailaddress@com.pl.eu.tooLong"
                },

                new UpdateUserDto()
                {
                    Email = "",
                    FirstName = "",
                    LastName = null
                }
            };

            return list.Select(e => new object[] { e });
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void UpdateUser_ValidRequest_Success(UpdateUserDto model)
        {
            var validator = new UpdateUserDtoValidator();


            var result = validator.TestValidate(model);


            result.ShouldNotHaveAnyValidationErrors();
        }


        [Theory]
        [MemberData(nameof(GetInvalidTestData))]
        public void UpdateUser_InvalidRequest_Fails(UpdateUserDto model)
        {
            var validator = new UpdateUserDtoValidator();

            var result = validator.TestValidate(model);

            result.ShouldHaveAnyValidationError();

        }


    }
}
