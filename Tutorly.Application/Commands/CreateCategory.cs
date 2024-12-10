using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;

namespace Tutorly.Application.Commands
{
    public class CreateCategory : ICommand
    {
        public Guid Id { get; }

        public string Name { get; set; }    

        public CreateCategory(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
