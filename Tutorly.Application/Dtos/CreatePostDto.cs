using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorly.Application.Dtos
{
    public class CreatePostDto
    {
        public int CategoryId { get; set; }
        public int MaxStudentAmount { get; set; }
        public int TutorId { get; set; }
        public string Description { get; set; } 
    }
}
