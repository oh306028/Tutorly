using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Handlers
{
    public class GetAllCategoriesHandler : IQueryHandler<GetAllCategories, IEnumerable<Category>>
    {
        private readonly IRepository<Category> _categoryRepository;

        public GetAllCategoriesHandler(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public async Task<IEnumerable<Category>> HandleAsync(GetAllCategories query)
        {
            var categories = await _categoryRepository.GetAllAsync();
            return categories;
        }


    }
}
