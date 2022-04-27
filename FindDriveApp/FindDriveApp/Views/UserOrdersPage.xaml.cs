using FindDriveApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FindDriveApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserOrdersPage : ContentPage
    {
        private readonly UserOrdersViewModel _userOrdersViewModel;
        public UserOrdersPage()
        {
            InitializeComponent();
            BindingContext = _userOrdersViewModel = Startup.Resolve<UserOrdersViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _userOrdersViewModel.OnAppearing();
        }
    }
}