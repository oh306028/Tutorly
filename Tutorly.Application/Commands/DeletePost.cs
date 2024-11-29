using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;

namespace Tutorly.Application.Commands
{
    public class DeletePost : ICommand
    {
        public Guid Id { get; set; }
        public int PostId { get; set; } 

        public DeletePost(int postId)
        {
            Id = Guid.NewGuid();
            PostId = postId;
        }
    }
}
