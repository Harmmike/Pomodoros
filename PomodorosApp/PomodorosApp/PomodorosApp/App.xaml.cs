using PomodorosApp.Services.Navigation;
using PomodorosApp.ViewModels;
using PomodorosApp.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PomodorosApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        private Task InitializeAsync()
        {
            var navService = ViewModelLocator.Resolve<INavigationService>();
            return navService.NavigateToAsync<DashboardViewModel>();
        }

        protected override async void OnStart()
        {
            await InitializeAsync();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
