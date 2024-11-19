using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Dtos.DisplayDtos
{
    public class TutorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Experience { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
    }
}
