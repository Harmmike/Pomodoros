using System;
using System.Collections.ObjectModel;

namespace PomodorosApp.Models
{
    public class PomodoroSet
    {
        public TimeSpan PomodoroLength { get; set; }
        public TimeSpan BreakLength { get; set; }
        public ObservableCollection<Pomodoro> Pomodoros { get; set; }

        public PomodoroSet()
        {
            Pomodoros = new ObservableCollection<Pomodoro>();
        }
    }
}
