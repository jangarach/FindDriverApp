using FindDriveApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindDriveApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        private readonly OrdersViewModel _ordersViewModel;
        public OrdersPage()
        {
            InitializeComponent();
            BindingContext = _ordersViewModel = Startup.Resolve<OrdersViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ordersViewModel.OnAppearing();
        }
    }
}