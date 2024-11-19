using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Dtos.DisplayDtos
{
    public class PostDto    
    {
        public int Id { get; set; }
        public int MaxStudentAmount { get; init; }
        public int CurrentStudentAmount => Students.Count;

        public readonly List<PostsStudents> Students = new();
        public string Description { get; set; }
        public DayOfWeek HappensOn { get; set; }
        public TimeSpan HappensAt { get; set; }

        public bool IsRemotely { get; set; }
        public bool IsAtStudentPlace { get; }
        public bool IsHappeningAtStudentPlace { get; set; }
        public Grade StudentsGrade { get; set; }

        public AddressDto Address { get; set; }
        public CategoryDto Category { get; set; }
    }
}
