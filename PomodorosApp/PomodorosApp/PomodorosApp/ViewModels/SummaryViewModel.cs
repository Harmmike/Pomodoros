using PomodorosApp.Models;
using PomodorosApp.Shared.Data;
using PomodorosApp.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace PomodorosApp.ViewModels
{
    public class SummaryViewModel : ViewModelBase
    {
        private IDataService _dataService;

        public ObservableCollection<PomodoroSet> SavedSets { get; set; }

        public SummaryViewModel(IDataService dataService)
        {
            _dataService = dataService;
            SavedSets = new ObservableCollection<PomodoroSet>();
        }

        public override async Task InitializeAsync(object navigationData = null)
        {
            SavedSets = await _dataService.GetSavedDataAsync();
            await base.InitializeAsync(navigationData);
        }
    }
}
