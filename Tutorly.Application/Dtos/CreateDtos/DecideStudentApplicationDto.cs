using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorly.Application.Dtos.CreateDtos
{
    public class DecideStudentApplicationDto
    {
        public int StudentId { get; set; }
        public bool IsAccepted { get; set; }    

        public DecideStudentApplicationDto(int studentId, bool isAccepted)
        {
            StudentId = studentId;
            IsAccepted = isAccepted;
        }

    }
}
