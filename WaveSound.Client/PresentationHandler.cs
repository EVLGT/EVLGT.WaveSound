using WaveSound.Client.Interfaces;
using WaveSound.Common.Constants;
using WaveSound.Domain.Interfaces;
using WaveSound.Domain.Exceptions;

namespace WaveSound.Client
{
    public class PresentationHandler : IPresentationHandler
    {
        private readonly IVideoConverter _videoConverter;

        public PresentationHandler(IVideoConverter videoConverter)
        {
            _videoConverter = videoConverter;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Blocker Bug", "S2190:Loops and recursions should not be infinite", Justification = "<Pending>")]
        public async Task ShowMenu()
        {
            while (true)
            {
                Console.WriteLine(@"---------- WaveSound ----------
    Choose your action:
        1. Convert SoundCloud to WaveSound (.wav)
        2. Close Software");

                var inputOption = EnterOption();
                await ProcessMenuInput(await inputOption);
            }
        }

        private async Task<int> EnterOption()
        {
            while (true)
            {
                if (IsValidOption(2, out int option)) { return option; }

                else { await MangageInvalidInput(); }
            }
        }

        private async Task MangageInvalidInput()
        {
            Console.WriteLine(ClientMessages.INVALID_MENUOPTION);

            var inputOption = EnterOption();
            await ProcessMenuInput(await inputOption);
        }

        private async Task ProcessMenuInput(int inputOption)
        {
            while (true)
            {
                try
                {
                    switch (inputOption)
                    {
                        case 1:
                            var videoUrl = GetSoundCloudUrl();
            
                            await _videoConverter.ConvertToWaveAsync(videoUrl);
                        break;

                        case 2:
                            Environment.Exit(0);
                        break;
                    }

                    break;
                }

                catch (BadUrlException)
                {
                    await Console.Out.WriteLineAsync(ClientMessages.BADURL_TEXT);
                }

                catch (TrackIsNullException) 
                {
                    await Console.Out.WriteLineAsync(ClientMessages.TRACKISNULL_TEXT);
                }
            }
        }

        private static bool IsValidOption(int maxOption, out int option)
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

        private static string GetSoundCloudUrl()
        {
            Console.Write("Enter SoundCloud Track URL: ");

            return Console.ReadLine() ?? "";
        }
    }
}
