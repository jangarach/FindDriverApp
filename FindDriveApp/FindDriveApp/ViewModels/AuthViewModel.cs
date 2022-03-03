using FindDriveApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FindDriveApp.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        //private const string AuthenticationUrl = "http://10.0.2.2:5000/api/authentication/mobileAuth?scheme=";
        private const string AuthenticationUrl = "http://finddriver.kg:5000/api/authentication/mobileAuth?scheme=";
        //private const string _AuthenticationUrl = "http://localhost:5000/api/authentication/mobileAuth?scheme=";

        private readonly IMessage _message;
        public Command SimpleAuthCommand { get; set; }
        public Command GoogleAuthCommand { get; set; }
        public Command FacebookAuthCommand { get; set; }
        public AuthViewModel(IMessage message)
        {
            _message = message;

            SimpleAuthCommand = new Command(async () => await OnAuthenticate("Simple"));
            GoogleAuthCommand = new Command(async () => await OnAuthenticate("Google"));
            FacebookAuthCommand = new Command(async () => await OnAuthenticate("Facebook"));
        }

        private async Task OnAuthenticate(string scheme)
        {
            try
            {
                var authUrl = new Uri(AuthenticationUrl + scheme);
                var callBackUrl = new Uri("xamarinapp://");
                var result = await WebAuthenticator.AuthenticateAsync(authUrl, callBackUrl);

                string authToken = result.AccessToken;
                string refreshToken = result.RefreshToken;
                string jwtTokenExpiresIn = result.Properties["jwt_token_expires"];

                var userInfo = new Dictionary<string, string>
                {
                    { "token", authToken },
                    { "name", $"{result.Properties["firstName"]} {result.Properties["secondName"]}"},
                    { "picture", HttpUtility.UrlDecode(result.Properties["picture"]) }
                };

                var url = "UserProfil" + '?' + string.Join("&", userInfo.Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));
                await AppShell.Current.GoToAsync(url);
            }
            catch (TaskCanceledException)
            {
                //Note: User exited auth flow;
            }
            catch (Exception e)
            {
                _message.DisplayAlert("Ошибка при авторизации", e.Message); 
            }
        }
    }
}
