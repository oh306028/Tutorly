using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Commands
{
    public class CreateAddress : ICommand
    {
        public Guid Id { get; }
        public int PostId { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }  
        public CreateAddress(int postId, string city, string street, string number)
        {
            Id = Guid.NewGuid();
            PostId = postId;
            City = city;
            Street = street;
            Number = number;
        }
    }
}
