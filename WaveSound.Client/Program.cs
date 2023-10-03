using WaveSound.Domain;

namespace WaveSound.Client
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var videoConverter = new VideoConverter();
            var presentationHandler = new PresentationHandler(videoConverter);

            presentationHandler.ShowMenu();
        }
    }
}