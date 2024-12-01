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
    public class UpdateUserDataHandler : IHandler<UpdateUserData>
    {
        private readonly IRepository<User> _userRepository;

        public UpdateUserDataHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task Handle(UpdateUserData command)
        {
            var userToUpdate = await _userRepository.GetByIdAsync(command.UserId);

            if (userToUpdate is null)
                throw new NotFoundException("User not found");

            userToUpdate.FirstName = command.FirstName ?? userToUpdate.FirstName;
            userToUpdate.LastName = command.LastName ?? userToUpdate.LastName;
            userToUpdate.Email = command.Email ?? userToUpdate.Email;


            if(userToUpdate is Tutor tutor)
            {
                tutor.Description = command.Description ?? tutor.Description;
                tutor.Experience = command.Experience ?? tutor.Experience;

            }else if(userToUpdate is Student student && command.Grade != null)
            {
                
                var gradeToChange = Enum.Parse<Grade>(command.Grade);
                student.Grade = gradeToChange;
                
            }

            await _userRepository.UpdateAsync(userToUpdate);
            


        }


    }
}
