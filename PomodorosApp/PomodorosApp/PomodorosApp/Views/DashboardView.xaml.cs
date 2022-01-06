using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PomodorosApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardView : TabbedPage
    {
        public DashboardView()
        {
            InitializeComponent();
        }
    }
}