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
    public class GetAllPostsHandler : IQueryHandler<GetAllPosts, IEnumerable<Post>>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Tutor> _tutorRepository;
        private readonly IRepository<Category> _categoryRepository;

        public GetAllPostsHandler(IRepository<Post> postRepository, IRepository<Tutor> tutorRepository, IRepository<Category> categoryRepository) 
        {
            _postRepository = postRepository;
            _tutorRepository = tutorRepository;
            _categoryRepository = categoryRepository;   
        }

        public async Task<IEnumerable<Post>> HandleAsync(GetAllPosts query)
        {
            var categories = await _categoryRepository.GetAllAsync();
            var posts = await _postRepository.GetAllAsync();
            var tutor = await _tutorRepository.GetAllAsync();


            posts = query.CategoryId is null ?
                          posts : posts.Where(c => c.CategoryId == query.CategoryId).ToList();

            return posts;
        }
    }
}
