using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Commands
{
    public class RegisterUser : ICommand
    {

        public RegisterUser(string email, string password, string confirmPassword, string firstName, string lastName, Role role, Grade? grade = null)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            Grade = grade;
        }


        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role Role { get; set; }

        public Grade? Grade { get; set; }


    }
}
