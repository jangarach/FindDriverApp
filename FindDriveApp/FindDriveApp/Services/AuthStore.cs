using FindDriveApp.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FindDriveApp.Services
{
    public class AuthStore : IAuthStore
    {
        public Guid UserId { get; private set; }
        public string AccessToken { get; private set; }
        public DateTime ExpiresToken { get; private set; }

        public bool IsAuthorized => CheckAuthorized();

        public async void Refresh()
        {
            var userIdTask = SecureStorage.GetAsync("userId");
            var accessTokenTask = SecureStorage.GetAsync("accessToken");
            var expiresTask = SecureStorage.GetAsync("expiresToken");

            await Task.WhenAll(userIdTask, accessTokenTask, expiresTask);
            SetPropsValues(userIdTask.Result, accessTokenTask.Result, expiresTask.Result);
        }

        public async void SetStore(string userId, string accessToken, string expiresToken)
        {
            var userIdTask = SecureStorage.SetAsync("userId", userId);
            var accessTokenTask = SecureStorage.SetAsync("accessToken", accessToken);
            var expiresTask = SecureStorage.SetAsync("expiresToken", expiresToken);

            await Task.WhenAll(userIdTask, accessTokenTask, expiresTask);

            SetPropsValues(userId, accessToken, expiresToken);
        }

        private void SetPropsValues(string userId, string accessToken, string expires)
        {
            UserId = !string.IsNullOrEmpty(userId) ? Guid.Parse(userId) : Guid.Empty;
            AccessToken = accessToken;
            DateTime outDateTime;
            if (DateTime.TryParseExact(expires, "dd.MM.yyyyHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out outDateTime))
            {
                ExpiresToken = outDateTime;
            }
            else
            {
                ExpiresToken = DateTime.Now.Add(TimeSpan.FromMinutes(5));
            }
        }

        private bool CheckAuthorized()
        {
            Refresh();
            if (string.IsNullOrEmpty(AccessToken) || UserId == Guid.Empty || ExpiresToken < DateTime.Now)
            {
                return false;
            }
            return true;
        }
    }
}
