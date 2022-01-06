using PomodorosApp.Services.Navigation;
using PomodorosApp.Views;
using System;
using System.Collections.Generic;
using TinyIoC;
using Xamarin.Forms;

namespace PomodorosApp.ViewModels.Base
{
    public class ViewModelLocator
    {
        private static TinyIoCContainer _container;
        private static Dictionary<Type, Type> _viewLookup;

        static ViewModelLocator()
        {
            _container = new TinyIoCContainer();
            _viewLookup = new Dictionary<Type, Type>();

            //Register ViewModels and Views
            Register<DashboardViewModel, DashboardView>();
            Register<SettingsViewModel, SettingsView>();
            Register<SummaryViewModel, SummaryView>();
            Register<WorkingViewModel, WorkingView>();

            //Register Services
            _container.Register<INavigationService, NavigationService>();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        public static Page CreatePageFor(Type viewModelType)
        {
            var viewType = _viewLookup[viewModelType];
            Page view = (Page)Activator.CreateInstance(viewType);
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
            return view;
        }

        private static void Register<TViewModel, TView>() where TViewModel : ViewModelBase where TView : Page
        {
            _viewLookup.Add(typeof(TViewModel), typeof(TView));
            _container.Register<TViewModel>();
        }
    }
}
