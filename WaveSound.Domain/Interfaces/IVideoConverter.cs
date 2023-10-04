namespace WaveSound.Domain.Interfaces
{
    public interface IVideoConverter
    {
        Task ConvertToWaveAsync(string videoUrl);
    }
}