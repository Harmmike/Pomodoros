using PomodorosApp.Services.Navigation;
using PomodorosApp.ViewModels.Base;
namespace PomodorosApp.ViewModels
{
    public class DashboardViewModel : ViewModelBase 
    {
        private INavigationService _navigationService;

        public DashboardViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
