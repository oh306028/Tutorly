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

        Task ApplyForPost(int postId);
    }

    public class PostService : IPostService
    {
        private readonly IHandler<CreatePost> _createPosthandler;
        private readonly IQueryHandler<GetAllPosts, IEnumerable<Post>> _queryHandler;
        private readonly IHandler<PostApply> _applyPostHandler;

        public PostService(IHandler<CreatePost> createPosthandler, IQueryHandler<GetAllPosts, IEnumerable<Post>> queryHandler, IHandler<PostApply> applyPostHandler)
        {
            _createPosthandler = createPosthandler;
            _queryHandler = queryHandler;
            _applyPostHandler = applyPostHandler;   
        }

        public async Task<IEnumerable<Post>> GetAll(QueryParams? queryParams = null)
        {
            var query = new GetAllPosts(queryParams);
            var posts = await _queryHandler.HandleAsync(query);
                
            return posts;   
        }


        public async Task Create(CreatePostDto dto)
        {
            var command = new CreatePost(
                dto.CategoryId,
                dto.TutorId,
                dto.MaxStudentAmount,
                dto.HappensOn,
                dto.HappensAt,
                dto.IsRemotely,
                dto.IsAtStudentPlace,
                dto.StudentsGrade,
                dto.Description,
                dto.AddressId);

            await _createPosthandler.Handle(command);

        }

        public async Task ApplyForPost(int postId)
        {
            var command = new PostApply(postId);
            await _applyPostHandler.Handle(command);

        }


    }
}
