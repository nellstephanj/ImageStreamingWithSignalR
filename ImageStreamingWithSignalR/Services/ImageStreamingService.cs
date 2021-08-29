using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ImageStreamingWithSignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace ImageStreamingWithSignalR.Services
{
    public class ImageStreamingService: IImageStreamingService
    {
        private readonly ILogger _logger;
        private readonly IHubContext<ImageStreamHub> _imageStreamHub;

        public ImageStreamingService(ILogger<ImageStreamingService> logger, IHubContext<ImageStreamHub> imageStreamHub)
        {
            _logger = logger;
            _imageStreamHub = imageStreamHub;
        }

        public async Task StreamImages()
        {
            while (true)
            {
                var filesList = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\Images");
                foreach (var filePath in filesList)
                {
                    _logger.LogInformation("Sending image {ImageName}", filePath);
                    var bytes = await File.ReadAllBytesAsync(filePath);
                    var content = new ImageContent
                    {
                        Binary = bytes,
                    };

                    Thread.Sleep(5000);
                    await _imageStreamHub.Clients.All.SendAsync("StreamImage", content);
                } 
            }
        }
    }
}