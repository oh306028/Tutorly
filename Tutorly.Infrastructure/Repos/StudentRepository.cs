using Microsoft.AspNetCore.Http;
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
    public class StudentRepository : IRepository<Student>
    {
        private readonly TutorlyDbContext _dbContext;

        public StudentRepository(TutorlyDbContext dbContext)
        {
            _dbContext = dbContext; 
        }
        public async Task AddAsync(Student student) 
        {
            await _dbContext.Users.AddAsync(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student student)    
        {
            _dbContext.Users.Remove(student);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            var results = await _dbContext.Users.OfType<Student>().ToListAsync();
            return results;
        }

        public async Task<IEnumerable<Student>> GetAllAsync(Expression<Func<Student, bool>> predicate)
        {
            var results = await _dbContext.Users.OfType<Student>().Where(predicate).ToListAsync();  
            return results;
        }

        public Student GetBy(Expression<Func<Student, bool>> predicate)
        {
            var result = _dbContext.Users.OfType<Student>().FirstOrDefault(predicate);
            return result;
        }

        public async Task<Student> GetByAsync(Expression<Func<Student, bool>> predicate)
        {
            var result = await _dbContext.Users.OfType<Student>().FirstOrDefaultAsync(predicate);
            return result;
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            var result = await _dbContext.Users.OfType<Student>().FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task UpdateAsync(Student student)    
        {
            _dbContext.Users.Update(student);
            await _dbContext.SaveChangesAsync();
        }
    }
}
