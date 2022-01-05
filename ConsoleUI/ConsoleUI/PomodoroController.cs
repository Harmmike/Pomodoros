using ConsoleUI.Models;
using System;
using System.Collections.Generic;
using System.Timers;

namespace ConsoleUI
{
    public class PomodoroController
    {
        private int _workInterval, _breakInterval, _currentPomodoroCount;
        private Timer _workTimer;
        private Timer _breakTimer;
        private List<Project> _projects;

        public Project CurrentProject { get; set; }

        public EventHandler WorkTimerDing;
        public EventHandler BreakTimerDing;
        public EventHandler PomodoroFinished;

        public PomodoroController()
        {
            _projects = new List<Project>();
            _currentPomodoroCount = 0;

            _workTimer = new Timer();
            _workTimer.Elapsed += OnWorkTimerElapsed;

            _breakTimer = new Timer();
            _breakTimer.Elapsed += OnBreakTimerElapsed;
        }

        public void StartNewPomodoro(int workInterval, int breakInterval, int totalPomodoros, string projectName, string projectDescription = "")
        {
            _breakInterval = (breakInterval * 1000) * 60;
            _workInterval = (workInterval * 1000) * 60;

            _workTimer.Interval = _workInterval;
            _breakTimer.Interval = _breakInterval;

            Project project = new Project()
            {
                PomodoroCount = totalPomodoros,
                ProjectName = projectName,
                ProjectDescription = projectDescription
            };

            _projects.Add(project);
            CurrentProject = project;

            _workTimer.Start();
        }

        public int GetWorkTime()
        {
            return (_workInterval / 60) / 1000;
        }

        public int GetBreakTime()
        {
            return (_breakInterval / 60) / 1000;
        }

        private void OnWorkTimerElapsed(object sender, ElapsedEventArgs args)
        {
            WorkTimerDing?.Invoke(this, new EventArgs());
            _workTimer.Stop();
            StartBreak();
        }

        private void OnBreakTimerElapsed(object sender, ElapsedEventArgs args)
        {
            BreakTimerDing?.Invoke(this, new EventArgs());
            _breakTimer.Stop();
            _workTimer.Start();
        }

        private void StartBreak()
        {
            _breakTimer.Start();
            _currentPomodoroCount++;
            if(_currentPomodoroCount >= CurrentProject.PomodoroCount)
            {
                PomodoroFinished?.Invoke(this, new EventArgs());
                _breakTimer.Stop();
                _workTimer.Stop();
            }
        }
    }
}
