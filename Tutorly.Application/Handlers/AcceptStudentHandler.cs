using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Authorization;
using Tutorly.Application.Commands;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;
using Tutorly.Domain.ModelsExceptions;
using Tutorly.WebAPI.Services;

namespace Tutorly.Application.Handlers
{
    public class AcceptStudentHandler : IHandler<AcceptStudent>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Student> _studentRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public AcceptStudentHandler(IRepository<Post> postRepository, IRepository<Student> studentRepository,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _postRepository = postRepository;
            _studentRepository = studentRepository;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        public async Task Handle(AcceptStudent command)
        {
            if (!command.IsAccepted)           
                return;
            
            var post = await _postRepository.GetByIdAsync(command.PostId);

            if (post is null)
                throw new NotFoundException("Post not found");

            if (post.Students == null || !post.Students.Any())
                throw new InvalidOperationException("Post does not have any students.");

            var user = _userContextService.User;
            var authorizationResult = await _authorizationService.AuthorizeAsync(user, post, new ResourceAvaibilityRequirement());

            if (!authorizationResult.Succeeded)
                throw new ForbidException("Cannot accept student that does not apply for that post");


            var student = await _studentRepository.GetByIdAsync(command.StudentId);

            if (student is null)
                throw new NotFoundException("Student not found");
    
            post.AcceptStudent(student);

            await _postRepository.UpdateAsync(post);
            

        }
    }
}
