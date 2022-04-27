using FindDriveApp.Infrastructure;
using FindDriveApp.Services.Interfaces;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FindDriveApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IMessage _message;
        private readonly IUserService _userService; 
        public Command SimpleAuthCommand { get; set; }
        public Command GoogleAuthCommand { get; set; }
        public Command FacebookAuthCommand { get; set; }
        public Command RegistrationCommand { get; set; }
        public LoginViewModel(IMessage message, IUserService userService)
        {
            _message = message;
            _userService = userService;

            SimpleAuthCommand = new Command(async () => await OnAuthenticate("Simple"));
            GoogleAuthCommand = new Command(async () => await OnAuthenticate("Google"));
            FacebookAuthCommand = new Command(async () => await OnAuthenticate("Facebook"));
            RegistrationCommand = new Command(async () => await OnRegistration());
        }

        private async Task OnAuthenticate(string scheme)
        {
            try
            {
                if (scheme == "Google")
                {
                    var result = await _userService.ExternalAuthAsync("Google");

                    if (result == false)
                    {
                        _message.DisplayAlert("Не удалось авторизоваться", "Попробуйте войти в систему через внутренний сервис.");
                    }
                    await Shell.Current.Navigation.PopModalAsync();
                    //await Shell.Current.GoToAsync("..");
                }
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

        private async Task OnRegistration()
        {

        }
    }
}
