using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorly.Application.Dtos.Params
{
    public class QueryParams
    {
        public int? CategoryId { get; set; }

        public bool? IncludePosts { get; set; }


        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
    }
}
