using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Queries
{
    public class GetUserData : IQuery<User>
    {
        public GetUserData(int userId)
        {
            UserId = userId;
        }
        public int UserId { get; set; } 
    }
}
