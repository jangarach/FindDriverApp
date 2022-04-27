using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;

namespace FindDriveApp.Droid
{
    [Activity(Label = "FindDriveApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            //Xamarin.Essentials.Platform.ActivityStateChanged += Platform_ActivityStateChanged;

            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();

            Xamarin.Essentials.Platform.OnResume(this);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);

            Xamarin.Essentials.Platform.OnNewIntent(intent);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            //Xamarin.Essentials.Platform.ActivityStateChanged -= Platform_ActivityStateChanged;
        }

        //void Platform_ActivityStateChanged(object sender, Xamarin.Essentials.ActivityStateChangedEventArgs e) =>
        //    Toast.MakeText(this, e.State.ToString(), ToastLength.Short).Show();


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    [Activity(NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(new[] { Intent.ActionView }, Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable }, DataScheme = "finddriveapp")]
    public class WebAuthenticationCallbackActivity : Xamarin.Essentials.WebAuthenticatorCallbackActivity
    {

    }
}