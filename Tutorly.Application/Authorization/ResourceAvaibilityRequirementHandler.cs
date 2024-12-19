using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;
using Tutorly.WebAPI.Services;

namespace Tutorly.Application.Authorization
{
    public class ResourceAvaibilityRequirementHandler : AuthorizationHandler<ResourceAvaibilityRequirement, Post>
    {
        private readonly IRepository<Post> _postRepository;


        public ResourceAvaibilityRequirementHandler(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;

        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceAvaibilityRequirement requirement, Post resource)
        {

            var post = await _postRepository.GetByIdAsync(resource.Id);
            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (post.TutorId == int.Parse(userId))
                context.Succeed(requirement);


        }
    }
}
