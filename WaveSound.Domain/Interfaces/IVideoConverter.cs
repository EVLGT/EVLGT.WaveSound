namespace WaveSound.Domain.Interfaces
{
    public interface IVideoConverter
    {
        void ConvertYoutubeToMp3(string videoUrl);

        void ConvertMp3ToWave(string mp3Path);
    }
}