using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Commands;
using Tutorly.Application.Dtos;
using Tutorly.Application.Handlers;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAll(QueryParams queryParams = null);
        Task Create(CreatePostDto dto);
    }

    public class PostService : IPostService
    {
        private readonly IHandler<CreatePost> _handler;
        private readonly IQueryHandler<GetAllPosts, IEnumerable<Post>> _queryHandler;
        public PostService(IHandler<CreatePost> handler, IQueryHandler<GetAllPosts, IEnumerable<Post>> queryHandler)
        {
            _handler = handler;
            _queryHandler = queryHandler;
        }

        public async Task<IEnumerable<Post>> GetAll(QueryParams? queryParams = null)
        {
            var query = new GetAllPosts(queryParams);
            var posts = await _queryHandler.HandleAsync(query);
                
            return posts;   
        }


        public async Task Create(CreatePostDto dto)
        {
            var command = new CreatePost(dto.CategoryId, dto.TutorId, dto.MaxStudentAmount, dto.Description);
            await _handler.Handle(command);

        }


    }
}
