using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Dtos.CreateDtos
{
    public class CreatePostDto
    {
        public int CategoryId { get; set; }
        public int TutorId { get; set; }
        public int MaxStudentAmount { get; set; }
        public DayOfWeek HappensOn { get; set; }
        public TimeSpan HappensAt { get; set; }
        public bool IsRemotely { get; set; }
        public bool IsAtStudentPlace { get; set; }
        public string Description { get; set; }
        public int? AddressId { get; set; }
        public Grade StudentsGrade { get; set; }
    }
}
