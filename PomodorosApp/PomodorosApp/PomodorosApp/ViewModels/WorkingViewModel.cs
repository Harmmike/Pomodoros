using PomodorosApp.ViewModels.Base;
using System;
using System.Threading.Tasks;
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
            set => SetProperty(ref _pomodorosInSet, value);
        }
        public int PomodorosLength
        {
            get => _pomodorosLength;
            set => SetProperty(ref _pomodorosLength, value);
        }
        public int BreakLength
        {
            get => _breakLength;
            set => SetProperty(ref _breakLength, value);
        }
        public ICommand BeginPomodoroCommand
        {
            get => _beginPomodoroCommand;
            set => SetProperty(ref _beginPomodoroCommand, value);
        }

        public WorkingViewModel()
        {
            BeginPomodoroCommand = new Command(BeginPomodoro);
            IsInactive = true;
        }

        public override Task InitializeAsync(object navigationData = null)
        {
            RunningTotal = new TimeSpan();
            return base.InitializeAsync(navigationData);
        }

        private void BeginPomodoro()
        {
            IsInactive = !IsInactive;
        }
    }
}
