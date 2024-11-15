using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Commands;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;
using Tutorly.Domain.ModelsExceptions;
using Tutorly.WebAPI.Services;

namespace Tutorly.Application.Handlers
{
    public class PostApplyHandler : IHandler<PostApply>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IUserContextService _userContextService;
        private readonly IRepository<Student> _studentRepository;   

        public PostApplyHandler(IRepository<Post> postRepository, IUserContextService userContextService, IRepository<Student> studentRepository)   
        {
            _postRepository = postRepository;
            _userContextService = userContextService;
            _studentRepository = studentRepository;   
        }

        public async Task Handle(PostApply command)
        {
            var post =  await _postRepository.GetByIdAsync(command.PostId);

            if (post is null)
                throw new NotFoundException("Post not found");


            var student = await _studentRepository.GetByIdAsync((int)_userContextService.GetUserId);

            if(student is null)
                throw new NotFoundException("Student not found");   


            post.AddStudent(student);      
            await _postRepository.UpdateAsync(post);
            
        }
    }
}
