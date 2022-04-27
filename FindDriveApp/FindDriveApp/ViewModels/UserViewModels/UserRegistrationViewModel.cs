using FindDriveApp.Infrastructure;
using FindDriveApp.Models;
using FindDriveApp.Services.Interfaces;
using System;
using Xamarin.Forms;

namespace FindDriveApp.ViewModels
{
    public class UserRegistrationViewModel : BaseViewModel
    {
        private readonly IMessage _message;
        private readonly IUserService _userService;

        private string _login;
        private string _password;
        private string _phoneNumber;
        private string _fullname;
        private bool _isValidPassword;
        private bool _isValidRepeatPassword;

        public Command RegistrationCommand { get; }
        public UserRegistrationViewModel(IMessage message, IUserService userService)
        {
            _message = message;
            _userService = userService;
            RegistrationCommand = new Command(OnRegistration, ValidateRegistration);

            this.PropertyChanged +=
                (_, __) => RegistrationCommand.ChangeCanExecute();
        }

        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public string FullName
        {
            get => _fullname;
            set => SetProperty(ref _fullname, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public bool IsValidPassword
        {
            get => _isValidPassword;
            set => SetProperty(ref _isValidPassword, value);
        }

        public bool IsValidRepeatPassword
        {
            get => _isValidRepeatPassword;
            set => SetProperty(ref _isValidRepeatPassword, value);
        }

        private async void OnRegistration()
        {
            try
            {
                var user = new User()
                {
                    UserName = Login,
                    FullName = FullName,
                    PhoneNumber = PhoneNumber,
                    Password = Password
                };

                await _userService.RegistrationAsync(user);
            }
            catch (Exception ex)
            {
                _message.DisplayAlert("Ошибка при регистраци!", ex.Message);
            }
        }

        private bool ValidateRegistration()
        {
            return !string.IsNullOrEmpty(Login) 
                && !string.IsNullOrEmpty(Password)
                && IsValidPassword 
                && IsValidRepeatPassword;
        }
    }
}
