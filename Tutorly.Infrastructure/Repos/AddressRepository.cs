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
    public class AddressRepository : IRepository<Address>
    {
        private readonly TutorlyDbContext _dbContext;

        public AddressRepository(TutorlyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Address address)   
        {
            await _dbContext.Address.AddAsync(address); 
            await _dbContext.SaveChangesAsync();    
        }

        public async Task DeleteAsync(Address address)
        {
            _dbContext.Remove(address);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Address>> GetAllAsync()
        {
            var results = await _dbContext.Address.ToListAsync();
            return results;
        }

        public async Task<IEnumerable<Address>> GetAllAsync(Expression<Func<Address, bool>> predicate)
        {
            var results = await _dbContext.Address.Where(predicate).ToListAsync();
            return results;
        }

        public Address GetBy(Expression<Func<Address, bool>> predicate)
        {
            var result = _dbContext.Address.FirstOrDefault(predicate);
            return result;
        }

        public async Task<Address> GetByAsync(Expression<Func<Address, bool>> predicate)
        {
            var result = await _dbContext.Address.FirstOrDefaultAsync(predicate);
            return result;
        }

        public async Task<Address> GetByIdAsync(int id)
        {
            var result = await _dbContext.Address.FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task UpdateAsync(Address address)
        {
            _dbContext.Address.Update(address);
            await _dbContext.SaveChangesAsync();
        }
    }
}
