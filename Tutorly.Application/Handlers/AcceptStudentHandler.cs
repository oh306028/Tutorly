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
    public class AcceptStudentHandler : IHandler<AcceptStudent>
    {
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<Student> _studentRepository;

        public AcceptStudentHandler(IRepository<Post> postRepository, IRepository<Student> studentRepository)
        {
            _postRepository = postRepository;
            _studentRepository = studentRepository;
        }
        public async Task Handle(AcceptStudent command)
        {
            if (!command.IsAccepted)           
                return;
            
            var post = await _postRepository.GetByIdAsync(command.PostId);

            if (post is null)
                throw new NotFoundException("Post not found");

            var student = await _studentRepository.GetByIdAsync(command.StudentId);

            if (student is null)
                throw new NotFoundException("Post not found");

            if (post.Students == null || !post.Students.Any())
                throw new InvalidOperationException("Post does not have any students.");

            post.AcceptStudent(student);

            await _postRepository.UpdateAsync(post);
            

        }
    }
}
