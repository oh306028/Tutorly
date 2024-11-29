using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Commands;
using Tutorly.Application.Dtos;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Dtos.DisplayDtos;
using Tutorly.Application.Dtos.Params;
using Tutorly.Application.Handlers;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Domain.Models;

namespace Tutorly.Application.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostWithTutorDto>> GetAll(QueryParams queryParams = null);
        Task Create(CreatePostDto dto);

        Task ApplyForPost(int postId);

        Task DeletePost(int postId);
    }

    public class PostService : IPostService
    {
        private readonly IHandler<CreatePost> _createPosthandler;
        private readonly IQueryHandler<GetAllPosts, IEnumerable<Post>> _queryHandler;
        private readonly IHandler<PostApply> _applyPostHandler;
        private readonly IMapper _mapper;
        private readonly IHandler<DeletePost> _deletePostHandler;

        public PostService(IHandler<CreatePost> createPosthandler, IQueryHandler<GetAllPosts, IEnumerable<Post>> queryHandler, IHandler<PostApply> applyPostHandler
            , IMapper mapper, IHandler<DeletePost> deletePostHandler)
        {
            _createPosthandler = createPosthandler;
            _queryHandler = queryHandler;
            _applyPostHandler = applyPostHandler;
            _mapper = mapper;
            _deletePostHandler = deletePostHandler;
        }

        public async Task<IEnumerable<PostWithTutorDto>> GetAll(QueryParams? queryParams = null)
        {
            var query = new GetAllPosts(queryParams);   
            var posts = await _queryHandler.HandleAsync(query);

            var mappedResults = _mapper.Map<List<PostWithTutorDto>>(posts);  
                
            return mappedResults;       
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

        public async Task DeletePost(int postId)
        {
            var command = new DeletePost(postId);
            await _deletePostHandler.Handle(command);
        }


    }
}
