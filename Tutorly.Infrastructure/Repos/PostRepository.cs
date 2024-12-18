using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Domain.Models;
using Tutorly.Infrastructure;

namespace Tutorly.Application.Interfaces
{
    public class PostRepository : IRepository<Post> 
    {
        private readonly TutorlyDbContext _dbContext;

        public PostRepository(TutorlyDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddAsync(Post post) 
        {
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Post post)
        {
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _dbContext.Posts.Include(p => p.Students).Include(p => p.Students).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllAsync(Expression<Func<Post, bool>> predicate)
        {
            var results = await _dbContext.Posts.Include(p => p.Students).Where(predicate).ToListAsync();
            return results;
        }

        public async Task<Post> GetByAsync(Expression<Func<Post, bool>> predicate)  
        {
            var result = await _dbContext.Posts.Include(p => p.Students).FirstOrDefaultAsync(predicate);

            return result;
        }

        public Post GetBy(Expression<Func<Post, bool>> predicate)
        {
            var result =  _dbContext.Posts.Include(p => p.Students).FirstOrDefault(predicate);

            return result;
        }   

        public Task<Post> GetByIdAsync(int id)
        {
            var result = _dbContext.Posts.Include(p => p.Students).FirstOrDefault(i => i.Id == id);
            return Task.FromResult(result);
        }
            
        public async Task UpdateAsync(Post post)  
        {
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }


    }
}
