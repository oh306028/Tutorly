using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutorly.Application.Dtos.DisplayDtos;
using Tutorly.Application.Dtos.Params;
using Tutorly.WebAPI.Services;

namespace Tutorly.WebAPI.Controllers
{
    [Route("api/tutors")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        private readonly ITutorService _tutorService;

        public TutorController(ITutorService tutorService)
        {
            _tutorService = tutorService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<TutorWithPostsDto>>> GetAllTutors([FromQuery] QueryParams queryParams)
        {
            var result = await _tutorService.GetAllTutors(queryParams);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TutorWithPostsDto>> GetTutorById([FromRoute] int id)
        {
            var result = await _tutorService.GetTutorById(id);  
            return Ok(result);
        }

    }
}
