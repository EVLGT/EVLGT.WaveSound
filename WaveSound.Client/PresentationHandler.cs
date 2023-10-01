using WaveSound.Client.Interfaces;
using WaveSound.Domain.Interfaces;

namespace WaveSound.Client
{
    public class PresentationHandler : IPresentationHandler
    {
        private readonly IVideoConverter _videoConverter;

        public PresentationHandler(IVideoConverter videoConverter)
        {
            _videoConverter = videoConverter;
        }

        public void OnBoarding()
        {
            var videoUrl = GetYoutubeVideoUrl();

            _videoConverter.ConvertToWave(videoUrl);
        }

        private string GetYoutubeVideoUrl()
        {
            Console.Write("Enter YouTube Video URL: ");

            return Console.ReadLine();
        }
    }
}
