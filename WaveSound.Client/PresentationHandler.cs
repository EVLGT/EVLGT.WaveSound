using WaveSound.Client.Interfaces;
using WaveSound.Common.Constants;
using WaveSound.Domain.Enums;
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
                Console.WriteLine(@"---------- WaveSound 3.23 ----------
    Choose your action:
        1. Convert SoundCloud to WaveSound (.wav) (for general use)
        2. Convert SoundCloud to Mp3 (.mp3) (for Serato DJ Pro)
        3. Close Software");

                var inputOption = EnterOption();
                await ProcessMenuInput(await inputOption);
            }
        }

        private async Task<int> EnterOption()
        {
            while (true)
            {
                if (IsValidOption(3, out int option)) { return option; }

                await ManageInvalidInput();
            }
        }

        private async Task ManageInvalidInput()
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
                            await _videoConverter.ConvertSoundcloudTrack(GetSoundCloudUrl(), FileType.Wave);
                        break;

                        case 2:
                            await _videoConverter.ConvertSoundcloudTrack(GetSoundCloudUrl(), FileType.Mp3);
                            break;

                        case 3:
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

            return false;
        }

        private static string GetSoundCloudUrl()
        {
            Console.Write("Enter SoundCloud Track URL: ");

            return Console.ReadLine() ?? "";
        }
    }
}
