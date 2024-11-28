using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Validators;
using Tutorly.Domain.Models;
using FluentValidation.TestHelper;
using System.Linq.Expressions;

namespace Tutorly.UnitTests
{
    public class RegisterUserDtoValidatorTest
    {
        private readonly Mock<IRepository<User>> userRepositoryMock;

        public RegisterUserDtoValidatorTest()
        {
            userRepositoryMock = new();
        }
        public static IEnumerable<object[]> GetExampleValidData()   
        {
            var list = new List<RegisterUserDto>()
            {
                new RegisterUserDto()
                {
                    Email = "test@mail.com",
                    Password = "password",
                    ConfirmPassword = "password",
                    FirstName = "testName",
                    LastName = "testLAstName",
                    Role = "Student"
                },

                new RegisterUserDto()
                {
                    Email = "test322@mail2311.com",
                    Password = "password322",
                    ConfirmPassword = "password322",
                    FirstName = "testName",
                    LastName = "testLAstName",
                    Role = "Student"
                }
            };




            return list.Select(q => new object[] { q });
        }

        public static IEnumerable<object[]> GetExampleInValidData() 
        {
            var list = new List<RegisterUserDto>()
            {
                new RegisterUserDto()
                {
                    Email = "testmail.com",
                    Password = "password",
                    ConfirmPassword = "password",
                    FirstName = "testName",
                    LastName = "testLAstName",
                    Role = "Student"
                },

                new RegisterUserDto()
                {
                    Email = "test322@mail2311.com",
                    Password = "password32",
                    ConfirmPassword = "password322",
                    FirstName = "testName",
                    LastName = "testLAstName",
                    Role = "Student"
                },

                 new RegisterUserDto()
                {
                    Email = "test322@mail2311.com",
                    Password = "password322",
                    ConfirmPassword = "password322",
                    FirstName = "testName",
                    LastName = "testLAstName",
                },

                  new RegisterUserDto()
                {
                    Email = "test322@mail2311.com",
                    Password = "password322",
                    ConfirmPassword = "password322",
                    FirstName = "TooLongfistNameHere",
                    LastName = "testLAstName",
                    Role = "Student"
                },

                   new RegisterUserDto()
                {
                    ConfirmPassword = "password322",
                    FirstName = "testName",
                    LastName = "testLAstName",
                    Role = "Student"
                },

                    new RegisterUserDto()
                {
                    Email = "test322@mail2311.com",
                    Password = "password322",
                    ConfirmPassword = "password322",
                    FirstName = "testName",
                    LastName = "TooLongLastNameHereeee",
                    Role = "Student"
                },

                     new RegisterUserDto()
                {

                },

                      new RegisterUserDto()
                {
                    Email = "test",
                    Password = "password322",
                    ConfirmPassword = "password322",
                    FirstName = "testName",
                    LastName = "testLAstName",
                    Role = "Student"
                }



            };




            return list.Select(q => new object[] { q });
        }



        [Theory]
        [MemberData(nameof(GetExampleValidData))]   
        public void RegisterUserDto_ValidData_NoErrors(RegisterUserDto model)
        {
            userRepositoryMock.Setup(s => s.GetBy(It.IsAny<Expression<Func<User, bool>>>()))
               .Returns((Expression<Func<User, bool>> predicate) =>
               {
                   return null; 
               });


            var validator = new RegisterUserDtoValidator(userRepositoryMock.Object);

            var response = validator.TestValidate(model);
            response.ShouldNotHaveAnyValidationErrors();

        }

        [Theory]
        [MemberData(nameof(GetExampleInValidData))]
        public void RegisterUserDTo_InvalidData_ThrowsErrors(RegisterUserDto model)
        {
            userRepositoryMock.Setup(s => s.GetBy(It.IsAny<Expression<Func<User, bool>>>()))
              .Returns((Expression<Func<User, bool>> predicate) =>
              {
                  return null;
              });
            var validator = new RegisterUserDtoValidator(userRepositoryMock.Object);

            var response = validator.TestValidate(model);
            response.ShouldHaveAnyValidationError();
        }


        [Fact]

        public void RegisterUserDto_ForTakenEmail_ThrowsValidationError()
        {
            var dto = new RegisterUserDto()
            {
                Email = "test@gmail.com",
                Password = "1234",
                ConfirmPassword = "1234",
                FirstName = "test",
                LastName = "test",
                Role = "Student"
            };

            userRepositoryMock.Setup(s => s.GetBy(It.IsAny<Expression<Func<User, bool>>>()))
                    .Returns(new Student { Email = dto.Email });


            var validator = new RegisterUserDtoValidator(userRepositoryMock.Object);

            var response = validator.TestValidate(dto);
            response.ShouldHaveValidationErrorFor(c => c.Email);
        }

    }
}
