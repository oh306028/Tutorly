using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Commands;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;
using Tutorly.Domain.ModelsExceptions;

namespace Tutorly.Application.Handlers
{
    public class RegisterUserHandler : IHandler<RegisterUser>
    {
        private readonly IRepository<User> _userRepository;

        public RegisterUserHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;   
        }

        public async Task Handle(RegisterUser command)
        {

            User user;

            if (command.Password != command.ConfirmPassword)    
                throw new WrongUserDataInputException("Password and confirm password has to be equal!");

            if(command.Role == Role.Tutor)
            {
                user = new Tutor()
                {
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    Email = command.Email,
                    Role = Role.Tutor,

                };
            }
            else
            {
                user = new Student()
                {
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    Email = command.Email,
                    Role = Role.Student,
                    Grade = (Grade)(command.Grade is null ? Grade.Primary : command.Grade)
                };
            }

            var hasher = new PasswordHasher();
            var hashedPassword = hasher.HashPassword(command.Password);
            user.PasswordHash = hashedPassword;
          
            await _userRepository.AddAsync(user);

        }

    }
}
