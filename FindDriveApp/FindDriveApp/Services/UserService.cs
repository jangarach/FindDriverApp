using FindDriveApp.Models;
using FindDriveApp.Services.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace FindDriveApp.Services
{
    public class UserService : IUserService
    {
        private const string AuthenticationUrl = "http://finddriver.kg:5000/api/auth/externalAuth?scheme=";
        private readonly IAuthStore _authStore;
        private readonly IHttpClientFactory _httpClientFactory;

        public UserService(IHttpClientFactory httpClientFactory, IAuthStore authStore)
        {
            _httpClientFactory = httpClientFactory;
            _authStore = authStore;
        }

        public Task<bool> InternalAuthAsync(User user)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> ExternalAuthAsync(string scheme)
        {
            var authUrl = new Uri(AuthenticationUrl + scheme);
            var callBackUrl = new Uri("finddriveapp://");
            var result = await WebAuthenticator.AuthenticateAsync(authUrl, callBackUrl);

            string authToken = result.AccessToken;
            string refreshToken = result.RefreshToken;
            string userId = result.Properties["id"];
            string expires = result.Properties["expires"];
            _authStore.SetStore(userId, authToken, expires);
            return true;
        }

        public async Task<User> RegistrationAsync(User user)
        {
            var httpClient = _httpClientFactory.CreateClient("baseHttpClient");

            var response = await httpClient.PostAsync("user/registrationUser/", new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseAsString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<User>(responseAsString); 
            }

            return default;
        }
    }
}
