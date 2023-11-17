using WaveSound.Domain;

namespace WaveSound.Client
{
    public static class Program
    {
        private static async Task Main()
        {
            Console.Title = "WaveSound v1-ALPHA by dariokrie";

            var soundCloudConverter = new SoundCloudConverter();
            var spotifyConverter = new SpotifyConverter();
            var presentationHandler = new PresentationHandler(soundCloudConverter, spotifyConverter);

            await presentationHandler.ShowMenu();
        }
    }
}
