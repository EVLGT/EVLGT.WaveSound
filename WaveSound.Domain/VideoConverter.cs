using WaveSound.Common.Constants;
using WaveSound.Domain.Interfaces;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;

namespace WaveSound.Domain
{
    public class VideoConverter : IVideoConverter // WIP
    {
        public async Task ConvertToMp3Async(string videoUrl)
        {
            var ytdl = new YoutubeDL
            {
                // set the path of yt-dlp and FFmpeg if they're not in PATH or current directory
                YoutubeDLPath = "yt-dlp.exe",
                FFmpegPath = "ffmpeg.exe",
                // optional: set a different download folder
                OutputFolder = SavePathConstants.FILE_SAVE_PATH
            };
            // download a video
            var res = await ytdl.RunAudioDownload(videoUrl, AudioConversionFormat.Mp3);
            // the path of the downloaded file
            string path = res.Data;
        }
    }
}