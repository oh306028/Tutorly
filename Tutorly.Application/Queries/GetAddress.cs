using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Queries
{
    public class GetAddress : IQuery<Address>
    {
        public int PostId { get; set; }
        public GetAddress(int postId)
        {
            PostId = postId;
        }
    }
}
