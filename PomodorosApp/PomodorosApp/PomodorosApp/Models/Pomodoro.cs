using PomodorosApp.Shared;
using System;

namespace PomodorosApp.Models
{
    public class Pomodoro : ObservableObject
    {
        public TimeSpan TotalTime { get; }
        public DateTime Date { get; set; }

        public Pomodoro(int minutes)
        {
            TotalTime = new TimeSpan(0, minutes, 0);
        }
    }
}
