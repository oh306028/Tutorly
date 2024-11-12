using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Commands
{
    public class PostApply : ICommand
    {
        public PostApply(int postId)
        {
            PostId = postId;
        }
        public Guid Id { get; set; }    
        public int PostId { get; init; }

    }
}
