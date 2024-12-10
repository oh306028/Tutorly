using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tutorly.Application.Dtos.CreateDtos
{
    public class CreateCategoryDto
    {
        public CreateCategoryDto(string name)
        {
            Name = name;
        }
        public string Name { get; set; }    
    }
}
