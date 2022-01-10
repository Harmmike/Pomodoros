using System.IO;
using System.Linq;
using System.Reflection;

namespace PomodorosApp.Services.Audio
{
    public class AudioService : IAudioService
    {
        private string _nextSound;
        private Assembly _assembly;

        public AudioService()
        {
            _assembly = typeof(App).GetTypeInfo().Assembly;
            _nextSound = "startAlarmSound.mp3";
        }

        public bool SetNextSound(string status = null)
        {
            if(status == null)
            {
                return false;
            }
            
            if(status.ToLower() == "active")
            {
                _nextSound = "breakAlarmSound.mp3";
                return true;
            }
            else if(status.ToLower() == "break")
            {
                _nextSound = "startAlarmSound.mp3";
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Play()
        {
            var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(GetStreamFromFile(_nextSound));
            player.Play();
        }

        private Stream GetStreamFromFile(string fileName)
        {
            var path = _assembly.GetManifestResourceNames().Single(str => str.EndsWith(fileName));
            var stream = _assembly.GetManifestResourceStream(path);
            return stream;
        }
    }
}
