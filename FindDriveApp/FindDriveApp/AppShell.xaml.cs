using FindDriveApp.ViewModels;
using FindDriveApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace FindDriveApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(FindOrderPage), typeof(FindOrderPage));
        }
    }
}
