using System.Threading.Tasks;

namespace ImageStreamingWithSignalR.Services
{
    public interface IImageStreamingService
    {
        Task StreamImages();
    }
}