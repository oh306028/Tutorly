using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos;
using Tutorly.Application.Dtos.Params;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Queries
{
    public class GetAllPosts : IQuery<IEnumerable<Post>>
    {
        public GetAllPosts(QueryParams? queryParams = null) 
        {
            CategoryId = queryParams.CategoryId;    
        }
        public int? CategoryId { get; init; }    
    }
}
