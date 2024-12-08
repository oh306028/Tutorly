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
    public class DeleteUserHandler : IHandler<DeleteUser>
    {
        private readonly IRepository<User> _userRepository;

        public DeleteUserHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(DeleteUser command)
        {
            var userToDelete = await _userRepository.GetByIdAsync(command.UserId);

            if (userToDelete is null)
                throw new NotFoundException("User to delete not found");

            await _userRepository.DeleteAsync(userToDelete);
        }
    }
}
