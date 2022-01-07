using System;
using System.Threading.Tasks;
using System.Timers;

namespace PomodorosApp.Services
{
    public enum TimerStatus
    {
        Waiting, Active, Break, Finished
    }

    public class PomodoroTimer : IPomodoroTimer
    {
        private int _pomodoroLength;
        private int _breakLength;
        private int _totalPomodoros;
        private int _currentPomodoros;
        private Timer _timer;
        private TimerStatus _status;

        public EventHandler<string> TimerStatusChanged { get; set; }

        public PomodoroTimer()
        {
            _timer = new Timer();
            _timer.Elapsed += OnTimerElapsed;
            _currentPomodoros = 1;
            _status = TimerStatus.Waiting;
        }

        public void SetBreakLength(int minutes)
        {
            _breakLength = minutes * 60000; //Convert to milliseconds for timer.
        }

        public void SetPomodoroLength(int minutes)
        {
            _pomodoroLength = minutes * 60000; //Convert to milliseconds for timer.
        }

        public void SetPomodoroSetQuantity(int total)
        {
            _totalPomodoros = total;
        }

        public void StartPomodoroTimer()
        {
            _status = TimerStatus.Active;
            _timer.Interval = _pomodoroLength;
            _timer.Start();
            RaiseStatusChange();
        }

        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            if(_currentPomodoros >= _totalPomodoros)
            {
                _status = TimerStatus.Finished;
            }
            switch (_status)
            {
                case TimerStatus.Active:
                    _timer.Interval = _breakLength;
                    _status = TimerStatus.Break;
                    break;
                case TimerStatus.Break:
                    _timer.Interval = _pomodoroLength;
                    _status = TimerStatus.Active;
                    _currentPomodoros++;
                    break;
                case TimerStatus.Finished:
                    _timer.Stop();
                    break;
            }
            RaiseStatusChange();
        }

        private void RaiseStatusChange()
        {
            TimerStatusChanged?.Invoke(this, _status.ToString());
        }
    }
}
