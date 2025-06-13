using ArtApi.Services;
using Kolokwium2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium2.Controllers
{
    [ApiController]
    [Route("api/galleries")]
    public class GalleriesController : ControllerBase
    {
        private readonly IGalleryService _galleryService;
    
        public GalleriesController(IGalleryService galleryService)
        {
            _galleryService = galleryService;
        }
    
        [HttpGet("{id}/exhibitions")]
        public async Task<IActionResult> GetGalleryExhibitions(int id)
        {
            var result = await _galleryService.GetGalleryWithExhibitionsAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}

