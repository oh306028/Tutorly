using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;

namespace Tutorly.Application.Commands
{
    public class UpdateUserData : ICommand
    {

        public UpdateUserData(int userId, string firstName = null, string lastName = null, string email = null, byte? experience = null, string grade = null, string description = null)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Experience = experience;
            Grade = grade;
            Description = description;
        }

        public int UserId { get; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }


        //Tutor
        public byte? Experience { get; set; }
        public string? Description { get; set; }

        //StudentOnly
        public string? Grade { get; set; }

        public Guid Id { get; }
    }
}
