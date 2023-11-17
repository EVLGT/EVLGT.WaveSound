using SpotifyExplode;
using WaveSound.Domain.Interfaces;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;

namespace WaveSound.Domain;

public class SpotifyConverter : ISpotifyConverter
{
    public async Task ConvertSpotifyTrack(string trackUrl)
    {
        var spotify = new SpotifyClient();
        var track = await spotify.Tracks.GetAsync(trackUrl);
        var savePath = FilePathUpdater.GetSavePath();

        using (HttpClient client = new())
        {
            try
            {
                var youtube = new YoutubeClient();
                var youtubeId = spotify.Tracks.GetYoutubeIdAsync(track.Id).Result;

                var streamManifest = await youtube.Videos.Streams.GetManifestAsync($"https://youtube.com/watch?v={youtubeId}");
                var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

                await youtube.Videos.Streams.DownloadAsync(streamInfo, Path.Combine(savePath, $"{track.Artists[0].Name} - {track.Title}.mp3"));
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }

    public async Task ConvertSpotifyPlaylist(string playlistUrl)
    {
        var spotify = new SpotifyClient();
        var playlist = await spotify.Playlists.GetAsync(playlistUrl);

        var tracks = playlist.Tracks;
        var tracksDownloading = 0;
        var couldNotInstall = string.Empty;

        var savePath = FilePathUpdater.GetSavePath();

        Directory.CreateDirectory(savePath + $"/{playlist.Name}/");

        foreach (var track in tracks)
        {
            tracksDownloading++;
            var author = track.Artists[0].Name;
            var title = track.Title;
            Console.WriteLine("Currently downloaded tracks: " + tracksDownloading + "\n" + "Downloading: " + author + " - " + title);

            using (HttpClient client = new())
            {
                try
                {
                    var youtube = new YoutubeClient();

                    var youtubeId = spotify.Tracks.GetYoutubeIdAsync(track.Id).Result;
                    var streamManifest = await youtube.Videos.Streams.GetManifestAsync($"https://youtube.com/watch?v={youtubeId}");
                    var streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();

                    await youtube.Videos.Streams.DownloadAsync(streamInfo, Path.Combine(savePath, playlist.Name, $"{author} - {title}.mp3"));
                }

                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                catch (Exception)
                {
                    var errorMessage = "Could not download: " + author + " - " + title;
                    var errorDetails = "\n" + author + " - " + title;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(errorMessage);
                    Console.ResetColor();

                    couldNotInstall += errorDetails;
                }
            }

            Console.WriteLine("Downloading is finished.");
        }

        Console.WriteLine($"Files the we couldn't download: {couldNotInstall}");
    }
}
