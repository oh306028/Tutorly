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
        Task<TutorWithPostsDto> GetTutorById(int id);
    }

    public class TutorService : ITutorService
    {
        private readonly IQueryHandler<GetAllTutors, IEnumerable<Tutor>> _getAllTutorsHandler;
        private readonly IMapper _mapper;
        private readonly IQueryHandler<GetTutorById, Tutor> _getTutorByIdHandler;

        public TutorService(IQueryHandler<GetAllTutors, IEnumerable<Tutor>> getAllTutorsHandler, IMapper mapper, IQueryHandler<GetTutorById, Tutor> getTutorByIdHandler)
        {
            _getAllTutorsHandler = getAllTutorsHandler;
            _mapper = mapper;
            _getTutorByIdHandler = getTutorByIdHandler;
        }

        public async Task<IEnumerable<TutorWithPostsDto>> GetAllTutors(QueryParams queryParams)
        {
            var query = new GetAllTutors(queryParams);
            var results = await _getAllTutorsHandler.HandleAsync(query);

            var mappedResults = _mapper.Map<IEnumerable<TutorWithPostsDto>>(results);
            return mappedResults;

        }

        public async Task<TutorWithPostsDto> GetTutorById(int id)
        {
            var query = new GetTutorById(id);
            var result = await _getTutorByIdHandler.HandleAsync(query);

            var mappedResult = _mapper.Map<TutorWithPostsDto>(result);

            return mappedResult;

        }



    }
}
