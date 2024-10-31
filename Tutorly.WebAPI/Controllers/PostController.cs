using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutorly.Application.Commands;
using Tutorly.Application.Dtos;
using Tutorly.Application.Handlers;
using Tutorly.Application.Interfaces;
using Tutorly.Application.Queries;
using Tutorly.Application.Services;
using Tutorly.Domain.Models;

namespace Tutorly.WebAPI.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService) 
        {
            _postService = postService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAll([FromBody] QueryParams? queryParams = null)  
        {

            var result = await _postService.GetAll(queryParams);
            return Ok(result);
                
        }
        
     


    }
}
