using System.Collections.ObjectModel;

namespace PomodorosApp.Models
{
    public class PomodoroSet
    {
        public ObservableCollection<Pomodoro> Pomodoros { get; set; }

        public PomodoroSet()
        {
            Pomodoros = new ObservableCollection<Pomodoro>();
        }
    }
}
