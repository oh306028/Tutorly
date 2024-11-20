using AutoMapper;
using Tutorly.Application.Dtos.DisplayDtos;
using Tutorly.Application.Dtos.Params;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Domain.Models;

namespace Tutorly.WebAPI.Services
{
    public interface ITutorService
    {
        Task<IEnumerable<TutorWithPostsDto>> GetAllTutors(QueryParams queryParams);
    }

    public class TutorService : ITutorService
    {
        private readonly IQueryHandler<GetAllTutors, IEnumerable<Tutor>> _getAllTutorsHandler;
        private readonly IMapper _mapper;

        public TutorService(IQueryHandler<GetAllTutors, IEnumerable<Tutor>> getAllTutorsHandler, IMapper mapper)
        {
            _getAllTutorsHandler = getAllTutorsHandler;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TutorWithPostsDto>> GetAllTutors(QueryParams queryParams)
        {
            var query = new GetAllTutors(queryParams);
            var results = await _getAllTutorsHandler.HandleAsync(query);

            var mappedResults = _mapper.Map<IEnumerable<TutorWithPostsDto>>(results);
            return mappedResults;

        }




    }
}
