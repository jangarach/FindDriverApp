using FindDriveApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace FindDriveApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}