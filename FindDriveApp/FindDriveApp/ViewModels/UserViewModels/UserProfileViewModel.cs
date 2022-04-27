using FindDriveApp.Infrastructure;
using FindDriveApp.Services.Interfaces;
using Xamarin.Forms;

namespace FindDriveApp.ViewModels
{
    public class UserProfileViewModel : BaseViewModel
    {
        private readonly IMessage _message;
        private readonly IAuthStore _authStore;
        private readonly IUserService _userService;
        public UserProfileViewModel(IMessage message, IAuthStore authStore, IUserService userService)
        {
            _message = message;
            _authStore = authStore;
            _userService = userService;
        }

        public override void OnAppearing()
        {
            _authStore.Refresh();
            if (_authStore.IsAuthorized == false)
            {

            }
        }
    }
}
