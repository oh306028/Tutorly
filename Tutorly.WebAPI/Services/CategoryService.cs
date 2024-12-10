using AutoMapper;
using Tutorly.Application.Commands;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Dtos.DisplayDtos;
using Tutorly.Application.Handlers;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Domain.Models;

namespace Tutorly.WebAPI.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategories();
        Task CreateCategory(CreateCategoryDto dto);
    }

        
    public class CategoryService : ICategoryService
    {
        private readonly IQueryHandler<GetAllCategories, IEnumerable<Category>> _getCategoriesHandler;
        private readonly IHandler<CreateCategory> _createCategoryHandler;
        private readonly IMapper _mapper;

        public CategoryService(IQueryHandler<GetAllCategories, IEnumerable<Category>> getCategoriesHandler, IHandler<CreateCategory> createCategoryHandler, IMapper mapper)
        {
            _getCategoriesHandler = getCategoriesHandler;
            _createCategoryHandler = createCategoryHandler;
            _mapper = mapper;
        }


        public async Task<IEnumerable<CategoryDto>> GetAllCategories()
        {
            var query = new GetAllCategories();
            var categories = await _getCategoriesHandler.HandleAsync(query);

            var result = _mapper.Map<IEnumerable<CategoryDto>>(categories);
            return result;

        }


        public async Task CreateCategory(CreateCategoryDto dto)
        {
            var command = new CreateCategory(dto.Name);
            await _createCategoryHandler.Handle(command);   
        }


    }
}
