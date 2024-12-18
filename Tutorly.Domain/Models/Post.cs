using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Domain.ModelsExceptions;

namespace Tutorly.Domain.Models
{
    public class Post   
    {

        public Post()
        {
            
        }
        public Post(Tutor tutor, int maxStudentAmount, Category category, DayOfWeek happensOn, TimeSpan happensAt, bool isRemotely, bool isAtStudentPlace, int? address = null, string? description = null)
        {
            if (maxStudentAmount <= 0)
                throw new ArgumentException("Post's students max amount must be greater than zero");

            MaxStudentAmount = maxStudentAmount;
            Description = description;
            Category = category;
            HappensOn = happensOn;
            HappensAt = happensAt;
            IsRemotely = isRemotely;
            IsAtStudentPlace = isAtStudentPlace;
            AddressId = address;
            Tutor = tutor;
        }

        public int Id { get; set; } 
        public int MaxStudentAmount { get; init; }
        public int CurrentStudentAmount => Students.Count;

        public readonly List<PostsStudents> Students = new();   
        public string? Description { get; set; }
        public DayOfWeek HappensOn { get; set; }
        private TimeSpan _happensAt;

        public TimeSpan HappensAt
        {
            get { return _happensAt; }
            set
            {
                if (value >= TimeSpan.MinValue && value < TimeSpan.MaxValue)
                {
                    _happensAt = value; 
                }
                else
                {
                    throw new ArgumentException("Post cannot be done at that time");
                }
            }
        }

        public bool IsRemotely { get; set; }
        public bool IsAtStudentPlace { get; }
        public bool IsHappeningAtStudentPlace { get; set; }
        public Grade StudentsGrade { get; set; }    

        public Address? Address { get; set; }
        public int? AddressId { get; set; }         

        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public void AddStudent(Student student)
        {
            if (CurrentStudentAmount >= MaxStudentAmount)
               throw new OutOfSpaceException("Current post cannot hold more students");

            Students.Add(new PostsStudents { PostId = Id, StudentId = student.Id });

        }

        public void DeleteStudent(Student student)
        {
            var postStudent = Students.FirstOrDefault(ps => ps.StudentId == student.Id);

            if (postStudent == null)
                throw new NotFoundException("Cannot delete student that is not a part of current post");

           
            Students.Remove(postStudent);

        } 
        

        public void AcceptStudent(Student student)
        {
            var postStudent = Students.FirstOrDefault(ps => ps.StudentId == student.Id);

            if (postStudent == null)
                throw new NotFoundException("Cannot accept student that does not belong to current post");

            postStudent.IsAccepted = true;
        }
    }
}
