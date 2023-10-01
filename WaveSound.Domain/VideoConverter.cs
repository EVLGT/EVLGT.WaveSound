using NAudio.Wave;
using WaveSound.Common;
using WaveSound.Domain.Interfaces;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace WaveSound.Domain
{
    public class VideoConverter : IVideoConverter // WIP
    {
        public async void ConvertToWave(string videoUrl)
        {
            var youtube = new YoutubeClient();
            var videoInfo = await youtube.Videos.GetAsync(videoUrl);
            var streamManifest = await youtube.Videos.Streams.GetManifestAsync(videoUrl);
            var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate(); //audioStream

            await youtube.Videos.Streams.DownloadAsync(streamInfo, SavePathConstants.FILE_SAVE_PATH);
        }
    }
}
