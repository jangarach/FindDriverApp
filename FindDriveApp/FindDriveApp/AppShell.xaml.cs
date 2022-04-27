using FindDriveApp.Views;
using Xamarin.Forms;

namespace FindDriveApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(OrderDetailPage), typeof(OrderDetailPage));
            Routing.RegisterRoute(nameof(FindOrderPage), typeof(FindOrderPage));
            Routing.RegisterRoute(nameof(UserRegistrationPage), typeof(UserRegistrationPage));
        }
    }
}
