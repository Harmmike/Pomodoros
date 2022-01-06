using System;

namespace PomodorosApp.Models
{
    public class Pomodoro
    {
        public TimeSpan TotalTime { get; }

        public Pomodoro(int minutes)
        {
            TotalTime = new TimeSpan(0, minutes, 0);
        }
    }
}
