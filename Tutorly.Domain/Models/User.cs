﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tutorly.Domain.Models
{
    public abstract class User
    {
        protected User()
        {
            
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Role Role{ get; set; }


        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
