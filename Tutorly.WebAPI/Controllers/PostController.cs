using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutorly.Application.Commands;
using Tutorly.Application.Handlers;
using Tutorly.Domain.Models;

namespace Tutorly.WebAPI.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
       
        [HttpGet]
        public ActionResult<IEnumerable<Post>> GetAll()
        {
         

        }


        [HttpPost("id")]
        public ActionResult ApplyForPost([FromRoute] int id)
        {

        }




    }
}
