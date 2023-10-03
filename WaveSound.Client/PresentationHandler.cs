using System.Transactions;
using WaveSound.Client.Interfaces;
using WaveSound.Common.Constants;
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

        public void ShowMenu()
        {
            Console.WriteLine(@"---------- WaveSound ----------
Choose your action:
    1. Convert YT to MP3
    2. Convert MP3 to WAVE
    3. Close Software");

            var inputOption = EnterOption();
            ProcessMenuInput(inputOption);
        }

        private int EnterOption()
        {
            while (true)
            {
                if (IsValidOption(3, out int option)) { return option; }

                else { MangageInvalidInput(); }
            }
        }

        private void MangageInvalidInput()
        {
            Console.WriteLine(ClientMessages.INVALID_MENUOPTION);

            var inputOption = EnterOption();
            ProcessMenuInput(inputOption);
        }

        private async Task ProcessMenuInput(int inputOption)
        {
            switch (inputOption)
            {
                case 1:
                    var videoUrl = GetYoutubeVideoUrl();
            
                    await _videoConverter.ConvertToMp3Async(videoUrl);
                break;

                case 2:
                    var mp3Path = GetMp3FilePath();

                    //_videoConverter.ConvertMp3ToWave(mp3Path);
                break;

                case 3:
                    Environment.Exit(0);
                break;
            }
        }

        private bool IsValidOption(int maxOption, out int option)
        {
            bool isValidOption = int.TryParse(Console.ReadLine(), out option);

            if (isValidOption && option >= 1 && option <= maxOption)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private string GetMp3FilePath()
        {
            Console.Write("Enter MP3 File Path: ");

            return Console.ReadLine();
        }

        private string GetYoutubeVideoUrl()
        {
            Console.Write("Enter YouTube Video URL: ");

            return Console.ReadLine();
        }
    }
}
