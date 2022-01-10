namespace PomodorosApp.Services.Audio
{
    public interface IAudioService
    {
        bool SetNextSound(string status);
        void Play();
    }
}
