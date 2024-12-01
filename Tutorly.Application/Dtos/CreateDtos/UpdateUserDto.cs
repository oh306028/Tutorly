using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorly.Application.Dtos.CreateDtos
{
    public class UpdateUserDto
    {
        public int UserId { get; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public byte? Experience { get; set; }
        public string? Description { get; set; }
        public string? Grade { get; set; }

    }
}
