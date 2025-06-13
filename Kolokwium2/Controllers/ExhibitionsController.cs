using ArtApi.Services;
using Kolokwium2.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium2.Controllers
{
    [ApiController]
    [Route("api/exhibitions")]
    public class ExhibitionsController : ControllerBase
    {
        private readonly IExhibitionService _exhibitionService;
    
        public ExhibitionsController(IExhibitionService exhibitionService)
        {
            _exhibitionService = exhibitionService;
        }
    
        [HttpPost]
        public async Task<IActionResult> AddExhibition([FromBody] ExhibitionCreateDto dto)
        {
            var success = await _exhibitionService.AddExhibitionAsync(dto);
            if (!success)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}

