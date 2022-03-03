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
    public partial class AuthPage : ContentPage
    {
        private readonly AuthViewModel _authViewModel;
        public AuthPage()
        {
            InitializeComponent();
            BindingContext = _authViewModel = Startup.Resolve<AuthViewModel>();
        }
    }
}