using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Domain.Models;
using Tutorly.Domain.ModelsExceptions;

namespace Tutorly.Application.Handlers
{
    public class GetUserDataHandler : IQueryHandler<GetUserData, User>
    {
        private readonly IRepository<User> _userRepository;

        public GetUserDataHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;

        }


        public async Task<User> HandleAsync(GetUserData query)
        {
           var userData = await _userRepository.GetByIdAsync(query.UserId);

            if (userData is null)
                throw new NotFoundException("User not found");

            return userData;
        }
    }
}
