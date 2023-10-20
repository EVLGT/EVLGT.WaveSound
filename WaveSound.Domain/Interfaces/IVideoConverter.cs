using WaveSound.Domain.Enums;

namespace WaveSound.Domain.Interfaces
{
    public interface IVideoConverter
    {
        Task ConvertSoundcloudTrack(string trackUrl, FileType type);
    }
}
