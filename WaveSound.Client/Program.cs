using WaveSound.Domain;

namespace WaveSound.Client
{
    public static class Program
    {
        private static async Task Main()
        {
            Console.Title = "WaveSound v1-ALPHA by dariokrie";

            var videoConverter = new VideoConverter();
            var presentationHandler = new PresentationHandler(videoConverter);

            await presentationHandler.ShowMenu();
        }
    }
}