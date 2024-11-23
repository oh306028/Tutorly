using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Domain.Models;
using Tutorly.Domain.ModelsExceptions;

namespace Tutorly.Application.Handlers
{
    public class GetTutorByIdHandler : IQueryHandler<GetTutorById, Tutor>
    {
        private readonly IRepository<Tutor> _tutorRepository;
        private readonly IRepository<Post> _postRepository;

        public GetTutorByIdHandler(IRepository<Tutor> tutorRepository, IRepository<Post> postRepository)
        {
            _tutorRepository = tutorRepository;
            _postRepository = postRepository;
        }

        public async Task<Tutor> HandleAsync(GetTutorById query)
        {
            var tutor = await _tutorRepository.GetByIdAsync(query.Id);
            var posts = await _postRepository.GetByAsync(t => t.TutorId == tutor.Id);

           if (tutor is null)
                throw new NotFoundException("Tutor not found");

            return tutor;

        }
    }
}
