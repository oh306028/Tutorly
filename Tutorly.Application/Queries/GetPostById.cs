using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Queries
{
    public class GetPostById : IQuery<Post>
    {
        public int PostId { get; set; } 

        public GetPostById(int id)
        {
            PostId = id;
        }
    }
}
