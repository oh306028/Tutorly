using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Commands;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;
using Tutorly.Domain.ModelsExceptions;
using Tutorly.WebAPI.Services;

namespace Tutorly.Application.Handlers
{
    public class UpdateUserDataHandler : IHandler<UpdateUserData>
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUserContextService _contextService;

        public UpdateUserDataHandler(IRepository<User> userRepository, IUserContextService contextService)
        {
            _userRepository = userRepository;
            _contextService = contextService;
        }


        public async Task Handle(UpdateUserData command)
        {
            var userToUpdate = await _userRepository.GetByIdAsync((int)_contextService.GetUserId);  

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
