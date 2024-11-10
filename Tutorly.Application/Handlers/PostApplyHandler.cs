using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Commands;
using Tutorly.Application.Interfaces;
using Tutorly.Domain.Models;
using Tutorly.Domain.ModelsExceptions;

namespace Tutorly.Application.Handlers
{
    public class PostApplyHandler : IHandler<PostApply>
    {
        private readonly IRepository<Post> _postRepository;

        public PostApplyHandler(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        //TO DO
        //user context accesor within the current user id
        public async Task Handle(PostApply command)
        {
            var post =  await _postRepository.GetByIdAsync(command.PostId);

            if (post is null)
                throw new NotFoundException("Post not found");


            // TO DO:
            //ADD CODE BELOW AFTER STUDENT REPO AND CONTEXT ACCESSOR

            /*
            var student = 

            post.AddStudent(student);
            
            await _postRepository.UpdateAsync();
            */

        }
    }
}
