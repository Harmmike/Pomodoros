using PomodorosApp.Shared;
using System;

namespace PomodorosApp.Models
{
    public class Pomodoro : ObservableObject
    {
        private bool _isComplete;

        public TimeSpan TotalTime { get; }
        public DateTime Date { get; }
        public bool IsComplete
        {
            get => _isComplete;
            set => SetProperty(ref _isComplete, value);
        }

        public Pomodoro(int minutes)
        {
            TotalTime = new TimeSpan(0, minutes, 0);
            Date = DateTime.Now;
            IsComplete = false;
        }
    }
}
