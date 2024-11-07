using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Dtos
{
    public class CreatePostDto
    {
        public int CategoryId { get; }
        public int TutorId { get; }
        public int MaxStudentAmount { get; }
        public DayOfWeek HappensOn { get; }
        public TimeSpan HappensAt { get; }
        public bool IsRemotely { get; }
        public bool IsAtStudentPlace { get; }
        public string? Description { get; }
        public int? AddressId { get; }
        public Grade StudentsGrade { get; }
    }
}
