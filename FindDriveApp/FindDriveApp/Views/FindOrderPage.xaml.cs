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
    public partial class FindOrderPage : ContentPage
    {
        private readonly FindOrderViewModel _findOrderViewModel;
        public FindOrderPage()
        {
            InitializeComponent();

            BindingContext = _findOrderViewModel = Startup.Resolve<FindOrderViewModel>();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _findOrderViewModel.OnAppearing();
        }
    }
}