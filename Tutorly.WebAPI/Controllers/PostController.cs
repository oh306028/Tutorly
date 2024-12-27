using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutorly.Application.Commands;
using Tutorly.Application.Dtos;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Dtos.DisplayDtos;
using Tutorly.Application.Dtos.Params;
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
        public async Task<ActionResult<IEnumerable<PostWithTutorDto>>> GetAll([FromQuery] QueryParams? queryParams = null)  
        {

            var result = await _postService.GetAll(queryParams);    
            return Ok(result);
                
        }

        [Authorize(Roles = "Tutor")]
        [HttpPost]  
        public async Task<ActionResult> Create(CreatePostDto dto)
        {
             await _postService.Create(dto);

            return Created("api/posts", null);
        }
            

        [HttpPost("{postId}")]
        [Authorize(Roles = "Student")]  
        public async Task<ActionResult> ApplyForPost([FromRoute] int postId)
        {
            await _postService.ApplyForPost(postId);

            return Ok();

        }

        [HttpDelete("{postId}")]
        [Authorize(Roles = "Tutor")]
        public async Task<ActionResult> DeletePost([FromRoute] int postId)
        {
            await _postService.DeletePost(postId);
            return Ok();
        }

        [HttpPost("decide/{postId}")]
        [Authorize(Roles = "Tutor")]
        public async Task<ActionResult> AcceptStudent([FromRoute] int postId, [FromBody] DecideStudentApplicationDto dto)      
        {
            await _postService.DecideStudentApplicationAsync(postId, dto); 

            return Accepted();
        }



    }
}
