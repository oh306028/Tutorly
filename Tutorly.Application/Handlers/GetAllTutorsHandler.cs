using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Dtos.DisplayDtos;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Handlers
{
    public class GetAllTutorsHandler : IQueryHandler<GetAllTutors, IEnumerable<Tutor>>
    {
        private readonly IRepository<Tutor> _tutorRepository;
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Category> _categoryRepository;

        public GetAllTutorsHandler(IRepository<Tutor> tutorRepository, IRepository<Post> postRepository, IRepository<Category> categoryRepository)
        {
           _tutorRepository = tutorRepository;
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;   
        }
        public async Task<IEnumerable<Tutor>> HandleAsync(GetAllTutors query)
        {
            var tutors = await _tutorRepository.GetAllAsync();
            var posts = await _postRepository.GetAllAsync();
            var categories = await _categoryRepository.GetAllAsync();


            if (query.IncludePosts == true)
            {
                foreach (var tutor in tutors)   
                {
                    tutor.Posts = null;
                }
            }
            
                    
            return tutors;

        }
    }
}
