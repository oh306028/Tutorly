using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Queries
{
    public class GetUserBy : IQuery<User>
    {
        public Expression<Func<User, bool>> Predicate { get; set; }

        public GetUserBy(Expression<Func<User, bool>> predicate)
        {
            Predicate = predicate;
        }
    }
}
