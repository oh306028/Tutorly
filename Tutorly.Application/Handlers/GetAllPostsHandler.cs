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
        private readonly IRepository<Address> _addressRepository;

        public GetAllPostsHandler(IRepository<Post> postRepository, IRepository<Tutor> tutorRepository, IRepository<Category> categoryRepository, IRepository<Address> addressRepository) 
        {
            _postRepository = postRepository;
            _tutorRepository = tutorRepository;
            _categoryRepository = categoryRepository;
            _addressRepository = addressRepository;
        }

        public async Task<IEnumerable<Post>> HandleAsync(GetAllPosts query)
        {
            var categories = await _categoryRepository.GetAllAsync();
            var posts = await _postRepository.GetAllAsync();
            var tutor = await _tutorRepository.GetAllAsync();
            var address = await _addressRepository.GetAllAsync();

            posts = query.CategoryId is null ?  
                          posts : posts
                          .Where(c => c.CategoryId == query.CategoryId);


            posts = query.City is null 
                            ? posts : posts
                                    .Where(a => a.Address.City.ToLower().Contains(query.City.ToLower()));


            posts = query.Street is null
                          ? posts : posts
                                  .Where(a => a.Address.Street.ToLower().Contains(query.Street.ToLower()));


            posts = query.Number is null    
                          ? posts : posts
                                  .Where(a => a.Address.Number.ToLower().Contains(query.Number.ToLower()));

            posts = query.IsRemotely is null
                    ? posts : posts.Where(r => r.IsRemotely == true).ToList();

            return posts;
        }
    }
}
