using FindDriveApp.Infrastructure;
using FindDriveApp.Models;
using FindDriveApp.Services.Interfaces;
using FindDriveApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FindDriveApp.ViewModels
{
    public class NewOrderViewModel : OrderBaseViewModel
    {
        private readonly IAuthStore _authStore;
        public Command CancelCommand { get; set; }
        public Command SaveCommand { get; set; }

        public NewOrderViewModel(IOrderService orderService,
                                 IOrderReferencesService orderReferencesService,
                                 IAuthStore authStore,
                                 IMessage message)
            :base(message)
        {
            _message = message;
            _authStore = authStore;
            _orderService = orderService;
            _orderReferencesService = orderReferencesService;

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public async override void OnAppearing()
        {
            try
            {
                if (Cities.Count > 0 && OrderTypes.Count > 0)
                    return;

                var citiesTask = _orderReferencesService.GetAllCities();
                var orderTypesTask = _orderReferencesService.GetAllOrderTypes();
                await Task.WhenAll(citiesTask, orderTypesTask);

                //Set cities
                Cities.Clear();
                var cities = await citiesTask;
                foreach (var city in cities)
                {
                    Cities.Add(city);
                }

                //Set order types
                OrderTypes.Clear();
                var orderTypes = await orderTypesTask;
                foreach (var orderType in orderTypes)
                {
                    OrderTypes.Add(orderType);
                }
            }
            catch (Exception ex)
            {
                _message.DisplayAlert("Ошибка при загрузке данных", ex.Message);
            }
        }

        

        private bool ValidateSave()
        {
            return SelectedFromCity != null
                && SelectedToCity != null
                && SelectedOrderType != null;
        }

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            try
            {
                if (_authStore.IsAuthorized == false)
                {
                    UnAuthenticateHandler();
                    return;
                }

                var order = new Order()
                {
                    FromCityId = SelectedFromCity.Id,
                    ToCityId = SelectedToCity.Id,
                    DateOut = SelectedOutDate + SelectedOutTime,
                    DateStamp = DateTime.Now,
                    OrderTypeId = SelectedOrderType.Id,
                    UserId = _authStore.UserId,
                    Comment = Comment,
                    PhoneNumber = PhoneNumber,
                    PassengersCount = PassengersCount
                };

                var response = await _orderService.CreateOrder(order, _authStore.AccessToken);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    UnAuthenticateHandler();
                    return;
                }
                else
                {
                    _message.LongToastAlert("Запись успешно добавлена!");
                    await Shell.Current.GoToAsync($"//{nameof(OrdersPage)}");
                }
            }
            catch (Exception ex)
            {
                _message.DisplayAlert("Ошибка во время добавления записи!", ex.Message);
            }
        }

        private async void UnAuthenticateHandler()
        {
            _message.LongToastAlert("Необходимо авторизоваться");
            await Shell.Current.Navigation.PushModalAsync(new LoginPage());
        }
    }
}
