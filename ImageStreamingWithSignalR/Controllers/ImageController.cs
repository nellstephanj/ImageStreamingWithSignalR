using System.Net.Mime;
using System.Threading.Tasks;
using ImageStreamingWithSignalR.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageStreamingWithSignalR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    public class ImageController : ControllerBase
    {
        private readonly IImageStreamingService _imageStreamingService;

        public ImageController(IImageStreamingService imageStreamingService)
        {
            _imageStreamingService = imageStreamingService;
        }
        
        [HttpPost("Start")]
        public async Task<IActionResult> StartStreaming()
        {
            await _imageStreamingService.StreamImages();
            return Ok();
        }
    }
}