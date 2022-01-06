using System.Threading.Tasks;

namespace PomodorosApp.Services.Navigation
{
    public interface INavigationService
    {
        Task GoBackAsync();
        Task NavigateToAsync<TViewModelBase>(object navigationData = null, bool setRoot = false);
    }
}
