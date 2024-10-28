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
        public int MaxStudentAmount { get; set; }
        public int CurrentStudentAmount => Students.Count;

        private readonly List<Student> Students = new();
        public string Description { get; set; } 

        public int TutorId { get; set; }
        public Tutor Tutor { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public void AddStudent(Student student)
        {
            if (CurrentStudentAmount >= MaxStudentAmount)
               throw new OutOfSpaceException("Current post cannot hold more students");

            Students.Add(student);

        }

        public void DeleteStudent(Student student)
        {
            if(!Students.Contains(student))
                throw new NotFoundException("Cannot delete student that is not a part of current post");

            Students.Remove(student);

        }
    }
}
