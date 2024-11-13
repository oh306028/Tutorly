using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;

namespace Tutorly.Application.Commands
{
    public class LoginUser : ICommand
    {
        public Guid Id { get; init; }
        public string Email { get; set; }
        public string Password { get; set; }    

        public LoginUser(string email, string password)
        {
            Id = Guid.NewGuid();
            Email = email;
            Password = password;
        }
    }
}
