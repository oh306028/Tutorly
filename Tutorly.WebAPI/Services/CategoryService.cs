using AutoMapper;
using Tutorly.Application.Dtos.DisplayDtos;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Domain.Models;

namespace Tutorly.WebAPI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategories();
    }

        
    public class CategoryService : ICategoryService
    {
        private readonly IQueryHandler<GetAllCategories, IEnumerable<Category>> _getCategoriesHandler;
        private readonly IMapper _mapper;

        public CategoryService(IQueryHandler<GetAllCategories, IEnumerable<Category>> getCategoriesHandler, IMapper mapper)
        {
            _getCategoriesHandler = getCategoriesHandler;
            _mapper = mapper;
        }


        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var query = new GetAllCategories();
            var categories = await _getCategoriesHandler.HandleAsync(query);

            var result = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return result;

        }



    }
}
