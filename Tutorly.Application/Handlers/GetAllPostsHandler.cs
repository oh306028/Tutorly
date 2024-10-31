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

        public GetAllPostsHandler(IRepository<Post> postRepository) 
        {
            _postRepository = postRepository;       
        }
        public async Task<IEnumerable<Post>> HandleAsync(GetAllPosts query)
        {

            var posts =
                query.CategoryId is null ?      
                await _postRepository.GetAllAsync() :           
                await _postRepository.GetAllAsync(x => x.CategoryId == query.CategoryId);

            return posts;

        }
    }
}
