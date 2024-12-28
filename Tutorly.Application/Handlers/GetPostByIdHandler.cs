using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Domain.Models;
using Tutorly.Domain.ModelsExceptions;

namespace Tutorly.Application.Handlers
{
    public class GetPostByIdHandler : IQueryHandler<GetPostById, Post>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Tutor> _tutorRepository;
        private readonly IRepository<Category> _categoryRepository; 

        public GetPostByIdHandler(IRepository<Post> postRepository, IRepository<Tutor> tutorRepository, IRepository<Category> categoryRepository)
        {
            _postRepository = postRepository;
            _tutorRepository = tutorRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<Post> HandleAsync(GetPostById query)
        {
            var post = await _postRepository.GetByIdAsync(query.PostId);

            if (post is null)
                throw new NotFoundException("Post not found");

            var tutor = await _tutorRepository.GetByIdAsync(post.TutorId);
            var category = _categoryRepository.GetByIdAsync(post.CategoryId);

            return post;

        }



    }
}
