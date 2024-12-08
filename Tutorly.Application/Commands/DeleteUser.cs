using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;

namespace Tutorly.Application.Commands
{
    public class DeleteUser : ICommand
    {
        public Guid Id { get; init; }
        public int UserId { get; set; } 

        public DeleteUser(int userId)   
        {
            Id = Guid.NewGuid();
            UserId = userId;
        }
    }
}
