using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos.DisplayDtos;
using Tutorly.Application.Dtos.Params;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Queries
{
    public class GetAllTutors : IQuery<IEnumerable<Tutor>>   
    {
        public GetAllTutors(QueryParams? queryParams = null)
        {
            IncludePosts = queryParams.IncludePosts;
        }
        public bool? IncludePosts { get; set; }
    }
}
