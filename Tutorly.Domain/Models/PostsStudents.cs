using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorly.Domain.Models
{
    public class PostsStudents
    {
        public int PostId { get; set; }
        public Post Post { get; set; }


        public int StudentId { get; set; }
        public Student Student { get; set; }

        public bool IsAccepted { get; set; } = false;
    }
}
