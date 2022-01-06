using PomodorosApp.ViewModels.Base;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PomodorosApp.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        public Task GoBackAsync()
        {
            return App.Current.MainPage.Navigation.PopAsync();
        }

        public async Task NavigateToAsync<TViewModelBase>(object navigationData = null, bool setRoot = false)
        {
            var view = ViewModelLocator.CreatePageFor(typeof(TViewModelBase));

            if (setRoot)
            {
                App.Current.MainPage = new NavigationPage(view);
            }
            else
            {
                if(App.Current.MainPage is NavigationPage navPage)
                {
                    await navPage.PushAsync(view);
                }
                else
                {
                    App.Current.MainPage = new NavigationPage(view);
                }
            }

            if(view.BindingContext is ViewModelBase vmBase)
            {
                await vmBase.InitializeAsync(navigationData);
            }
        }
    }
}
