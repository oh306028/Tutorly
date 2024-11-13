using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;

namespace Tutorly.Infrastructure.Repos
{
    public class TutorRepository : IRepository<Tutor>
    {
        private readonly TutorlyDbContext _dbContext;

        public TutorRepository(TutorlyDbContext dbContext)
        {
            _dbContext = dbContext;
        }   

        public async Task AddAsync(Tutor tutor)     
        {
            await _dbContext.Users.AddAsync(tutor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Tutor tutor) 
        {
            _dbContext.Users.Remove(tutor);
            await _dbContext.SaveChangesAsync();
        }

        public Task<IEnumerable<Tutor>> GetAllAsync()
        {
            var results = _dbContext.Users.OfType<Tutor>().ToList();
            return Task.FromResult((IEnumerable<Tutor>)results);
        }
            
        public Task<IEnumerable<Tutor>> GetAllAsync(Expression<Func<Tutor, bool>> predicate)
        {
            var results = _dbContext.Users.OfType<Tutor>().Where(predicate).ToList();

            return Task.FromResult((IEnumerable<Tutor>)results);
        }

        public async Task<Tutor> GetBy(Expression<Func<Tutor, bool>> predicate)
        {
            var result = await _dbContext.Users.OfType<Tutor>().FirstOrDefaultAsync(predicate);
            return result;
        }

        public Task<Tutor> GetByIdAsync(int id)
        {
            var tutor = _dbContext.Users.OfType<Tutor>().FirstOrDefault(i => i.Id == id);

            return Task.FromResult(tutor);
        }
            
        public async Task UpdateAsync(Tutor tutor)
        {
            _dbContext.Users.Update(tutor);
            await _dbContext.SaveChangesAsync();
        }

  
    }
}
