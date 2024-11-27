using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Validators;
using Tutorly.Domain.Models;
using Tutorly.Domain.ModelsExceptions;

namespace Tutorly.UnitTests
{
    public class PostTests  
    {

        [Fact]
        public void CreatePost_ValidModel_CreatesThePost()
        {          
            var postValidator = new CreatePostDtoValidator();
            var createPost = new CreatePostDto()
            {
                TutorId = 1,
                CategoryId = 1,
                MaxStudentAmount = 1,
                IsRemotely = true,
                IsAtStudentPlace = false,
                Description = "test",
                StudentsGrade = Grade.Secondary,
                HappensOn = DayOfWeek.Monday,
                HappensAt = TimeSpan.Parse("17:30")

            };


            var response = postValidator.Validate(createPost);


            response.IsValid.Should().BeTrue();

        }





        [Fact]
        public void AddStudent_ForValidRequest_IncreasesThePostsStudentsCount()
        {
            var post = new Post()
            {

                MaxStudentAmount = 2

            };

            var student = new Student();
            post.AddStudent(student);


            post.CurrentStudentAmount.Should().Be(1);

        }



        [Fact]
        public void AddStudentForPost_ValidPostStudentAmountRequest_GoesThroughWithoutException()
        {

            var post = new Post()   
            {

                MaxStudentAmount = 5

            };

            var student = new Student();        



            try {   
                post.AddStudent(student);

            }catch(OutOfSpaceException ex)
            {
                Assert.Fail("unexpected exception was thrown");
            }
            

        }

        [Fact]
        public void AddStudentForPost_NotEnoughSpaceForMoreStudentst_ThrowsException()
        {   
            var post = new Post()
            {

                MaxStudentAmount = 2,
                
            };
            var student1 = new Student();
            var student2 = new Student();

            post.AddStudent(student1);
            post.AddStudent(student2);

            var applyingStudent = new Student();


            try
            {
                post.AddStudent(applyingStudent);
                Assert.Fail("unexpected exception was thrown");
            }
            catch (OutOfSpaceException ex)
            {
                
            }

        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(7)]
        [InlineData(70)]
        public void Addstudent_EnoughSpaceForStudent_GoesThrough(int maxStudentPostAmount)
        {
            var post = new Post()
            {

                MaxStudentAmount = maxStudentPostAmount,

            };
            var student1 = new Student();
            post.AddStudent(student1);
            var applyingStudent = new Student();


            try
            {
                post.AddStudent(applyingStudent);
                
            }
            catch (OutOfSpaceException ex)
            {
                Assert.Fail("unexpected exception was thrown");
            }

        }



    }
}
