using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Tutorly.Application.Interfaces;
using ICommand = Tutorly.Application.Interfaces.ICommand;

namespace Tutorly.Application.Commands
{
    public class CreatePost : ICommand
    {
        public CreatePost(int categoryId, int tutorId, int maxStudentAmount, string? description = null)
        {
            Id = Guid.NewGuid();
            CategoryId = categoryId;
            TutorId = tutorId;
            MaxStudentAmount = maxStudentAmount;
            Description = description;
        }

        public Guid Id { get; }
        public int CategoryId { get; }
        public int TutorId { get; }
        public int MaxStudentAmount { get; }
        public string Description { get; }


    }
}
