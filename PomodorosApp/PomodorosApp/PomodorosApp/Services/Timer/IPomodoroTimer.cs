using System;
using System.Threading.Tasks;

namespace PomodorosApp.Services
{
    public interface IPomodoroTimer
    {
        EventHandler<string> TimerStatusChanged { get; set; }
        void SetPomodoroLength(int minutes);
        void SetBreakLength(int minutes);
        void SetPomodoroSetQuantity(int total);
        void StartPomodoroTimer();
    }
}
