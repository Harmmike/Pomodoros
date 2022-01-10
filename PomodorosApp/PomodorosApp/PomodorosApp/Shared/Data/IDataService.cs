using PomodorosApp.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PomodorosApp.Shared.Data
{
    public interface IDataService
    {
        Task<ObservableCollection<PomodoroSet>> GetSavedDataAsync();
        Task<bool> SaveCompletedSetAsync(PomodoroSet pomodoroSet);
    }
}
