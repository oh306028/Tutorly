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
    public class LoginUserHandler : IHandler<LoginUser>
    {
        private readonly IRepository<User> _userRepository;

        public LoginUserHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(LoginUser command)
        {
            var userToLogIn = await _userRepository.GetBy(e => e.Email == command.Email);

            if (userToLogIn is null)
                throw new NotFoundException("Incorrect email or password");

            var hasher = new PasswordHasher();
            var result = hasher.VerifyHashedPassword(userToLogIn.PasswordHash, command.Password);

            if (result == PasswordVerificationResult.Failed)
                throw new NotFoundException("Incorrect email or password");

        }
    }
}
