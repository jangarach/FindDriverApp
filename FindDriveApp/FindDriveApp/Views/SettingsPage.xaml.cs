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
    public partial class SettingsPage : ContentPage
    {
        private readonly SettingsViewModel _settingsViewModel;
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = _settingsViewModel = Startup.Resolve<SettingsViewModel>();
        }
    }
}