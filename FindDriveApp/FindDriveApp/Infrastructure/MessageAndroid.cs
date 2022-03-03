using Android.App;
using Android.Widget;

namespace FindDriveApp.Infrastructure
{
    public class MessageAndroid : IMessage
    {
        public void LongToastAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortToastAlert(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }

        public async void DisplayAlert(string title, string message)
        {
            await App.Current.MainPage.DisplayAlert(title, message, "Ok");
        }
    }
}
