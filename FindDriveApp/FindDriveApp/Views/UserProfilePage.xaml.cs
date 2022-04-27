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
    public partial class UserProfilePage : ContentPage
    {
        private readonly UserProfileViewModel _userProfileViewModel;
        public UserProfilePage()
        {
            InitializeComponent();
            BindingContext = _userProfileViewModel = Startup.Resolve<UserProfileViewModel>();
        }
    }
}