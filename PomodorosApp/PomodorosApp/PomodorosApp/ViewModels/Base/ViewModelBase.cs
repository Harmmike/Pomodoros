using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PomodorosApp.ViewModels.Base
{
    public class ViewModelBase : BindableObject
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

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if(EqualityComparer<T>.Default.Equals(storage, value)) { return false; }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
