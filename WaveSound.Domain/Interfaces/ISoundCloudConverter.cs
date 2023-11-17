using WaveSound.Domain.Enums;

namespace WaveSound.Domain.Interfaces
{
    public interface ISoundCloudConverter
    {
        Task ConvertSoundcloudTrack(string trackUrl, FileType type);
    }
}
