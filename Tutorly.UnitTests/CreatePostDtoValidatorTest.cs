using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Commands;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Validators;
using Tutorly.Domain.Models;

namespace Tutorly.UnitTests
{
    public class CreatePostDtoValidatorTest
    {

        public static IEnumerable<object[]> GetValidationSampleData()
        {
            var list = new List<CreatePostDto>() {

                new CreatePostDto(){
                TutorId = 1,    
                CategoryId = 1,
                MaxStudentAmount = 1,
                IsRemotely = true,
                IsAtStudentPlace = false,
                Description = "test",
                StudentsGrade = Grade.Secondary,
                HappensOn = DayOfWeek.Monday,
                HappensAt = TimeSpan.Parse("17:30")
                },

                 new CreatePostDto(){
                TutorId = 5,
                CategoryId = 5,
                MaxStudentAmount = 10,
                IsRemotely = false,
                IsAtStudentPlace = true,
                Description = "test23332",
                StudentsGrade = Grade.HighSchool,
                HappensOn = DayOfWeek.Sunday,
                HappensAt = TimeSpan.Parse("15:45")
                },


                  new CreatePostDto(){
                TutorId = 10,
                CategoryId = 10,
                MaxStudentAmount = 8,
                IsRemotely = false,
                IsAtStudentPlace = false,
                Description = "test3",
                StudentsGrade = Grade.College,
                HappensOn = DayOfWeek.Wednesday,
                HappensAt = TimeSpan.Parse("8:00")
                }

            };

            return list.Select(q => new object[] { q });

        }   

        [Theory]
        [MemberData(nameof(GetValidationSampleData))]
        public void CreatePost_ValidModel_CreatesThePost(CreatePostDto model)
        {
            var postValidator = new CreatePostDtoValidator();


            var response = postValidator.TestValidate(model);


            response.ShouldNotHaveAnyValidationErrors();

        }
    }
}
