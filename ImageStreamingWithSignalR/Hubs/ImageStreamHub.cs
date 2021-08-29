using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace ImageStreamingWithSignalR.Hubs
{
    public class ImageStreamHub : Hub
    {
        public async Task SendImage(ImageContent image)
        {
            await Clients.All.SendAsync("StreamImage", image);
        }
    }
}