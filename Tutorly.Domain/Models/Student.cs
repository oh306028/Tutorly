using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorly.Domain.Models
{
    public class Student : User
    {
        public Grade Grade { get; set; }

        public List<Post> Posts { get; set; } = new();
    }
}
