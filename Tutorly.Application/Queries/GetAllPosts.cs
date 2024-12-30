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
            City = queryParams.City;
            Street = queryParams.Street;
            Number = queryParams.Number;
            IsRemotely = queryParams.IsRemotely;
        }
        public int? CategoryId { get; init; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public bool? IsRemotely { get; set; }   
    }
}
