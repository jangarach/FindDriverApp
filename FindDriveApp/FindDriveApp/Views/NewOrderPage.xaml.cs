using FindDriveApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindDriveApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewOrderPage : ContentPage
    {
        private readonly NewOrderViewModel _orderAddViewModel;
        public NewOrderPage()
        {
            InitializeComponent();
            _orderAddViewModel = Startup.Resolve<NewOrderViewModel>();
            BindingContext = _orderAddViewModel;
        }

        protected override void OnAppearing()
        {
            _orderAddViewModel?.OnAppearing();
        }
    }
}