using FindDriveApp.Services.Interfaces;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FindDriveApp.ViewModels
{
    /// <summary>
    /// Настройки используются только для тестовой сборки в релизной версии необходимо отключить.
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        private readonly IAuthStore _authStore;
        public Command ClearSecureStoreCommand { get; private set; }
        public SettingsViewModel(IAuthStore authStore)
        {
            _authStore = authStore;
            ClearSecureStoreCommand = new Command(OnClearSecureStore);
        }

        private void OnClearSecureStore()
        {
            SecureStorage.RemoveAll();
        }
    }
}
