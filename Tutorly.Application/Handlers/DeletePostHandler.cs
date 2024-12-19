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
    public class DeletePostHandler : IHandler<DeletePost>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public DeletePostHandler(IRepository<Post> postRepository, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _postRepository = postRepository;
           _authorizationService = authorizationService;
           _userContextService = userContextService;
        }

        public async Task Handle(DeletePost command)
        {

            var postToDelete = await _postRepository.GetByIdAsync(command.PostId);

            if (postToDelete is null)
                throw new NotFoundException("Post not found");

            var user = _userContextService.User;

            var authorizationResult = await _authorizationService.AuthorizeAsync(user, postToDelete, new ResourceAvaibilityRequirement());

            if (!authorizationResult.Succeeded)
                throw new ForbidException("Cannot delete post that does not belong to that tutor");


            await _postRepository.DeleteAsync(postToDelete);

        }


    }
}
