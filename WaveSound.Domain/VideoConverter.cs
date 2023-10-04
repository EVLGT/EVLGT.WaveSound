using SoundCloudExplode;
using WaveSound.Common.Constants;
using WaveSound.Domain.Exceptions;
using WaveSound.Domain.Interfaces;

namespace WaveSound.Domain
{
    public class VideoConverter : IVideoConverter
    {
        public async Task ConvertToWaveAsync(string videoUrl)
        {
            var soundcloud = new SoundCloudClient();

            if (await soundcloud.Tracks.IsUrlValidAsync(videoUrl))
            {
                var track = await soundcloud.Tracks.GetAsync(videoUrl) ?? throw new TrackIsNullException();
                var trackName = PathEx.EscapeFileName(track.Title!);
                var trackPath = Path.Join(SavePathConstants.FILE_SAVE_PATH, $"{trackName}.wav");

                await soundcloud.DownloadAsync(track, trackPath);

                await Console.Out.WriteLineAsync($"Save to: " + trackPath);
            }

            else
            {
                throw new BadUrlException();
            }
        }
    }
}