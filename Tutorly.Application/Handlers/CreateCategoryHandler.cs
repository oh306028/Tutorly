using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Commands;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Handlers
{
    public class CreateCategoryHandler : IHandler<CreateCategory>
    {
        private readonly IRepository<Category> _categoryRepository;

        public CreateCategoryHandler(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(CreateCategory command)
        {

            var category = new Category()
            {
                Name = command.Name

            };

            await _categoryRepository.AddAsync(category);


        }
    }
}
