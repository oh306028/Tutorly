using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Dtos.DisplayDtos;
using Tutorly.WebAPI.Services;

namespace Tutorly.WebAPI.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("{id}/address")]
        public async Task<ActionResult<AddressDto>> GetPostAddress([FromRoute]int id)
        {
            var result = await _addressService.GetPostAddressAsync(id); 
            return Ok(result);
        }

        [HttpPost("{id}/address")]
        public async Task<ActionResult> CreateAddressToPost([FromRoute]int postId, [FromBody]CreateAddressDto dto)
        {
            await _addressService.CreateAddressAsync(postId, dto);
            return Created("api/posts", null);
        }

    }
}
