﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Interfaces
{
    public class PostRepository : IRepository<Post> 
    {
        public Task AddAsync(Post post) 
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
            
        public Task<IEnumerable<Post>> GetAllAsync(Predicate<Post> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
            
        public Task UpdateAsync(Post post)  
        {
            throw new NotImplementedException();
        }



    }
}
