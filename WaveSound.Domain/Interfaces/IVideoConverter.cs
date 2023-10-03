namespace WaveSound.Domain.Interfaces
{
    public interface IVideoConverter
    {
        Task ConvertToMp3Async(string videoUrl);
    }
}