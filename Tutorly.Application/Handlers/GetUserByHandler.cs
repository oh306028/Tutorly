using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Handlers
{
    public class GetUserByHandler : IQueryHandler<GetUserBy, User>
    {
        private readonly IRepository<User> _userRepository;

        public GetUserByHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;   
        }
        public async Task<User> HandleAsync(GetUserBy query)
        {
            var user = await _userRepository.GetByAsync(query.Predicate);

            return user;    

        }


    }
}
