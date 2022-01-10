using PomodorosApp.Models;
using PomodorosApp.Services;
using PomodorosApp.Services.Audio;
using PomodorosApp.Shared.Data;
using PomodorosApp.ViewModels.Base;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;

namespace PomodorosApp.ViewModels
{
    public class WorkingViewModel : ViewModelBase
    {
        private bool _isInactive;
        private TimeSpan _runningTotal;
        private DateTime _currentStartTime;
        private int _pomodorosInSet;
        private int _pomodorosLength;
        private int _breakLength;
        private string _statusText;
        private PomodoroSet _currentSet;
        private IPomodoroTimer _timer;
        private Timer _runningTimer;

        private IAudioService _audioService;
        private IDataService _dataService;

        private ICommand _beginPomodoroCommand;

        public bool IsInactive
        {
            get => _isInactive;
            set => SetProperty(ref _isInactive, value);
        }
        public TimeSpan RunningTotal
        {
            get => _runningTotal;
            set => SetProperty(ref _runningTotal, value);
        }
        public DateTime CurrentStartTime
        {
            get => _currentStartTime;
            set => SetProperty(ref _currentStartTime, value);
        }
        public int PomodorosInSet
        {
            get => _pomodorosInSet;
            set
            {
                if(value > 0) { SetProperty(ref _pomodorosInSet, value); }
            }
        }
        public int PomodoroLength
        {
            get => _pomodorosLength;
            set
            {
                if (value > 0) { SetProperty(ref _pomodorosLength, value); }
            }
        }
        public int BreakLength
        {
            get => _breakLength;
            set
            {
                if (value > 0) { SetProperty(ref _breakLength, value); }
            }
        }
        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }
        public PomodoroSet CurrentSet
        {
            get => _currentSet;
            set => SetProperty(ref _currentSet, value);
        }
        public IPomodoroTimer Timer
        {
            get => _timer;
            set => SetProperty(ref _timer, value);
        }

        public ICommand BeginPomodoroCommand
        {
            get => _beginPomodoroCommand;
            set => SetProperty(ref _beginPomodoroCommand, value);
        }

        public WorkingViewModel(IDataService dataService)
        {
            BeginPomodoroCommand = new Command(BeginPomodoro);
            Timer = new PomodoroTimer();
            this.Timer.TimerStatusChanged += OnTimerStatusChanged;
            _runningTimer = new Timer();
            _runningTimer.Enabled = false;
            _runningTimer.Interval = 1000;
            _runningTimer.Elapsed += OnRunningTimerTick;

            IsInactive = true;
            PomodorosInSet = 4;
            PomodoroLength = 25;
            BreakLength = 5;

            _audioService = new AudioService();
            _dataService = dataService;
        }

        public override Task InitializeAsync(object navigationData = null)
        {
            RunningTotal = new TimeSpan();
            return base.InitializeAsync(navigationData);
        }

        private void BeginPomodoro()
        {
            Timer.SetBreakLength(BreakLength);
            Timer.SetPomodoroLength(PomodoroLength);
            Timer.SetPomodoroSetQuantity(PomodorosInSet);

            _runningTimer.Enabled = true;
            _runningTimer.Start();

            IsInactive = !IsInactive;
            CurrentStartTime = DateTime.Now;
            CurrentSet = new PomodoroSet()
            {
                BreakLength = new TimeSpan(0, BreakLength, 0),
                PomodoroLength = new TimeSpan(0, PomodoroLength, 0),
                Date = DateTime.Now
            };

            CurrentSet.Pomodoros.Add(new Pomodoro(PomodoroLength) { Date = DateTime.Now });

            Timer.StartPomodoroTimer();
        }

        private void OnTimerStatusChanged(object sender, string newStatus)
        {
            _audioService.Play();
            StatusText = newStatus;
            RunningTotal = TimeSpan.Zero;

            if(StatusText.ToLower() == "finished")
            {
                _runningTimer.Stop();
                _audioService.SetNextSound(string.Empty);
                OnSetComplete();
                return;
            }
            else
            {
                _audioService.SetNextSound(StatusText);
            }
        }

        private void OnRunningTimerTick(object sender, ElapsedEventArgs e)
        {
            RunningTotal += TimeSpan.FromSeconds(1);
        }

        private async void OnSetComplete()
        {
            //save
            bool saved = await _dataService.SaveCompletedSetAsync(CurrentSet);

            if (!saved)
            {
                //TODO: Log error
                //TODO: Queue re-save
            }

            //reset
            IsInactive = true;
            PomodorosInSet = 4;
            PomodoroLength = 25;
            BreakLength = 5;
            Timer = new PomodoroTimer();
            RunningTotal = TimeSpan.Zero;
            CurrentSet = null;
        }
    }
}
