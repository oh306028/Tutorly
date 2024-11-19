using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorly.Application.Dtos.DisplayDtos
{
    public class TutorWithPostsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte Experience { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public List<PostDto> Posts { get; set; } = new();
    }
}
