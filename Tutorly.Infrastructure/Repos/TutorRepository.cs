using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;

namespace Tutorly.Infrastructure.Repos
{
    public class TutorRepository : IRepository<Tutor>
    {
        public Task AddAsync(Tutor entity)  
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id) 
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Tutor>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Post>> GetAllAsync(Predicate<Tutor> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<Tutor> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
            
        public Task UpdateAsync(Tutor entity)
        {
            throw new NotImplementedException();
        }
    }
}
