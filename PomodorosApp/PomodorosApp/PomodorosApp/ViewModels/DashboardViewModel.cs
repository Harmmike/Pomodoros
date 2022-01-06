using PomodorosApp.Services.Navigation;
using PomodorosApp.ViewModels.Base;
using System.Threading.Tasks;

namespace PomodorosApp.ViewModels
{
    public class DashboardViewModel : ViewModelBase 
    {
        private SettingsViewModel _settingsVM;
        private SummaryViewModel _summaryVM;
        private WorkingViewModel _workingVM;

        private INavigationService _navigationService;

        public SettingsViewModel SettingsVM
        {
            get => _settingsVM;
            set => SetProperty(ref _settingsVM, value);
        }
        public SummaryViewModel SummaryVM
        {
            get => _summaryVM;
            set => SetProperty(ref _summaryVM, value);
        }
        public WorkingViewModel WorkingVM
        {
            get => _workingVM;
            set => SetProperty(ref _workingVM, value);
        }

        public DashboardViewModel(INavigationService navigationService, SettingsViewModel settingsVM, SummaryViewModel summaryVM, WorkingViewModel workingVM)
        {
            _navigationService = navigationService;
            SettingsVM = settingsVM;
            SummaryVM = summaryVM;
            WorkingVM = workingVM;
        }

        public override Task InitializeAsync(object navigationData = null)
        {
            return Task.WhenAny(base.InitializeAsync(navigationData), SettingsVM.InitializeAsync(null), SummaryVM.InitializeAsync(null), WorkingVM.InitializeAsync(null));
        }
    }
}
