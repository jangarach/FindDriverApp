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
    public partial class UserRegistrationPage : ContentPage
    {
        private readonly UserRegistrationViewModel _userRegistrationViewModel;
        public UserRegistrationPage()
        {
            InitializeComponent();
            BindingContext = _userRegistrationViewModel = Startup.Resolve<UserRegistrationViewModel>();
        }
    }
}