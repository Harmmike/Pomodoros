using PomodorosApp.Shared;
using System.Threading.Tasks;

namespace PomodorosApp.ViewModels.Base
{
    public class ViewModelBase : ObservableObject
    {
        private string _title;
        private bool _isLoading;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public virtual Task InitializeAsync(object navigationData = null)
        {
            return Task.CompletedTask;
        }
    }
}
