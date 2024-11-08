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
    public class CategoryRepository : IRepository<Category>
    {
        private readonly TutorlyDbContext _dbContext;

        public CategoryRepository(TutorlyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Category entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(Category entity)
        {
            _dbContext.Categories.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }   

        public  async Task<IEnumerable<Category>> GetAllAsync()
        {
            var results = await _dbContext.Categories.ToListAsync();
            return results;
        }

        public Task<IEnumerable<Category>> GetAllAsync(Expression<Func<Category, bool>> predicate)
        {
           var results =  _dbContext.Categories.Where(predicate);
             
            return Task.FromResult((IEnumerable<Category>)results.ToList());    
        }

        public Task<Category> GetByIdAsync(int id)
        {
            var category = _dbContext.Categories.FirstOrDefault(i => i.Id == id);

            return Task.FromResult(category);
        }   

        public async Task UpdateAsync(Category entity)
        {
            _dbContext.Categories.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
