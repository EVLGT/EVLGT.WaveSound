using SoundCloudExplode;
using WaveSound.Domain.Enums;
using WaveSound.Domain.Exceptions;
using WaveSound.Domain.Interfaces;

namespace WaveSound.Domain
{
    public class SoundCloudConverter : ISoundCloudConverter
    {
        public async Task ConvertSoundcloudTrack(string trackUrl, FileType type)
        {
            var soundcloud = new SoundCloudClient();

            if (await soundcloud.Tracks.IsUrlValidAsync(trackUrl))
            {
                var track = await soundcloud.Tracks.GetAsync(trackUrl) ?? throw new TrackIsNullException();
                var trackName = PathEx.EscapeFileName(track.Title!);
                var trackPath = await GetTrackPath(type, trackName);

                await soundcloud.DownloadAsync(track, trackPath);

                await Console.Out.WriteLineAsync($"Save to: " + trackPath);
            }

            else
            {
                throw new BadUrlException();
            }
        }

        private async Task<string> GetTrackPath(FileType type, string trackName)
        {
            string saveFilePath = FilePathUpdater.GetSavePath();

            return type switch
            {
                FileType.Wave => Path.Join(saveFilePath, $"{trackName}.wav"),
                FileType.Mp3 => Path.Join(saveFilePath, $"{trackName}.mp3"),
                _ => string.Empty
            };
        }
    }
}
