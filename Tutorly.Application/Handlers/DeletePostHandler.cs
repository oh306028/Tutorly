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
    public class DeletePostHandler : IHandler<DeletePost>
    {
        private readonly IRepository<Post> _postRepository;

        public DeletePostHandler(IRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task Handle(DeletePost command)
        {
            var postToDelete = await _postRepository.GetByIdAsync(command.PostId);

            if (postToDelete is null)
                throw new NotFoundException("Post not found");

            await _postRepository.DeleteAsync(postToDelete);

        }


    }
}
