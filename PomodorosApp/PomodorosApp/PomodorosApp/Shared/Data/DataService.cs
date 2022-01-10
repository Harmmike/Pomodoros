using PomodorosApp.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace PomodorosApp.Shared.Data
{
    public class DataService : IDataService
    {
        private readonly string SAVE_FILE_NAME = "Pomodoros.txt";

        public ObservableCollection<PomodoroSet> Pomodoros { get; set; }

        public DataService()
        {
            Pomodoros = new ObservableCollection<PomodoroSet>();
        }

        public Task<ObservableCollection<PomodoroSet>> GetSavedDataAsync()
        {
            try
            {
                string path = GetFilepath();

                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                string[] csvData = File.ReadAllLines(path);
                for (int i = 0; i < csvData.Length; i++)
                {
                    string[] data = csvData[i].Split(',');
                    for (int j = 0; j < data.Length; j++)
                    {
                        PomodoroSet pomodorosSet = new PomodoroSet();
                        pomodorosSet.Date = DateTime.Parse(data[0]);
                        pomodorosSet.PomodoroLength = new TimeSpan(0, int.Parse(data[2]), 0);
                        pomodorosSet.BreakLength = new TimeSpan(0, int.Parse(data[3]), 0);

                        for (int k = 0; k < int.Parse(data[1]); k++)
                        {
                            Pomodoro pomodoro = new Pomodoro(pomodorosSet.PomodoroLength.Minutes)
                            {
                                Date = pomodorosSet.Date
                            };
                            pomodorosSet.Pomodoros.Add(pomodoro);
                        }
                        Pomodoros.Add(pomodorosSet);
                    }
                }
            }
            catch (Exception e)
            {
                //Log error
            }

            return Task.FromResult(Pomodoros);
        }

        public async Task<bool> SaveCompletedSetAsync(PomodoroSet pomodoroSet)
        {
            bool isSuccess = false;
            try
            {
                string path = GetFilepath();
                ObservableCollection<PomodoroSet> existingData = await GetSavedDataAsync();

                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                string[] allSavedData = new string[existingData.Count];
                for (int i = 0; i < existingData.Count; i++)
                {
                    allSavedData[i] = $"{existingData[i].Date},{existingData[i].Pomodoros.Count},{existingData[i].PomodoroLength},{existingData[i].BreakLength}";
                }

                allSavedData[allSavedData.Length] = $"{pomodoroSet.Date},{pomodoroSet.Pomodoros.Count},{pomodoroSet.PomodoroLength},{pomodoroSet.BreakLength}";

                File.WriteAllLines(path, allSavedData);

                isSuccess = true;
            }
            catch (Exception e)
            {
                isSuccess = false;
            }
            return isSuccess;
        }

        private string GetFilepath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), SAVE_FILE_NAME);
        }
    }
}
