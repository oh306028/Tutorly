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

            var posts =
                query.CategoryId is null ?      
                await _postRepository.GetAllAsync() :           
                await _postRepository.GetAllAsync(x => x.CategoryId == query.CategoryId);

            var tutors = await _tutorRepository.GetAllAsync();

            var categories = await _categoryRepository.GetAllAsync();


            var postsWithTutors = posts.Join(
                tutors,                               
                post => post.TutorId,               
                tutor => tutor.Id,                   
                (post, tutor) =>                     
                {
                    post.Tutor = tutor;
                    return post;
                });

            var postsWithCategories = postsWithTutors.Join(
                categories,                          
                post => post.CategoryId,             
                category => category.Id,             
                (post, category) =>                  
                {
                    post.Category = category;
                    return post;
                });

            return postsWithCategories;

        }
    }
}
