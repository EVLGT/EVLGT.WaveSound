namespace WaveSound.Domain.Interfaces;

public interface ISpotifyConverter
{
    Task ConvertSpotifyTrack(string trackUrl);

    Task ConvertSpotifyPlaylist(string playlistUrl);
}
