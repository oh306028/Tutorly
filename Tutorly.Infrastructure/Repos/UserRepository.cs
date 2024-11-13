using Microsoft.EntityFrameworkCore;
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
    public class UserRepository : IRepository<User>
    {
        private readonly TutorlyDbContext _dbContext;

        public UserRepository(TutorlyDbContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public async Task AddAsync(User user)
        {
           await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var results = await _dbContext.Users.ToListAsync();
            return results;
        }

        public async Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> predicate)
        {
            var results = await _dbContext.Users.Where(predicate).ToListAsync();
            return results;
        }

        public async Task<User> GetBy(Expression<Func<User, bool>> predicate)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(predicate);

            return result;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var result = await _dbContext.Users.FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public Task UpdateAsync(User user)  
        {
            _dbContext.Users.Update(user);

            return Task.CompletedTask;
        }
    }
}
