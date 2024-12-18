using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;

namespace Tutorly.Application.Commands
{
    public class AcceptStudent : ICommand
    {
        public Guid Id { get; }
        public int PostId { get; }
        public int StudentId { get; }
        public bool IsAccepted { get; set; }     

        public AcceptStudent(int postId, int studentId,  bool isAccepted)
        {
            Id = Guid.NewGuid();
            PostId = postId;
            StudentId = studentId;
            IsAccepted = isAccepted;
        }

    }
}
