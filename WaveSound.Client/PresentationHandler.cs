using WaveSound.Client.Interfaces;
using WaveSound.Common.Constants;
using WaveSound.Domain;
using WaveSound.Domain.Enums;
using WaveSound.Domain.Interfaces;
using WaveSound.Domain.Exceptions;

namespace WaveSound.Client
{
    public class PresentationHandler : IPresentationHandler
    {
        private readonly ISoundCloudConverter _soundCloudConverter;
        private readonly ISpotifyConverter _spotifyConverter;

        public PresentationHandler(ISoundCloudConverter soundCloudConverter, ISpotifyConverter spotifyConverter)
        {
            _soundCloudConverter = soundCloudConverter;
            _spotifyConverter = spotifyConverter;
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
        3. Convert Spotify to Mp3
        4. Convert Spotify Playlist to Mp3
        5. Set Download File Path");

                var inputOption = EnterOption();
                await ProcessMenuInput(await inputOption);
            }
        }

        private async Task<int> EnterOption()
        {
            while (true)
            {
                if (IsValidOption(5, out int option)) { return option; }

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
                            await _soundCloudConverter.ConvertSoundcloudTrack(GetUrl(), FileType.Wave);
                        break;

                        case 2:
                            await _soundCloudConverter.ConvertSoundcloudTrack(GetUrl(), FileType.Mp3);
                            break;

                        case 3:
                            await _spotifyConverter.ConvertSpotifyTrack(GetUrl());
                            break;

                        case 4:
                            await _spotifyConverter.ConvertSpotifyPlaylist(GetUrl());
                            break;

                        case 5:
                            FilePathUpdater.UpdateFilePath(GetPath());
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

        private string GetPath()
        {
            Console.Write("Save File Path: ");
            return Console.ReadLine();
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

        private static string GetUrl()
        {
            Console.Write("Enter Track URL: ");

            return Console.ReadLine() ?? "";
        }
    }
}
