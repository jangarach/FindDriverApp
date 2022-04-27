using FindDriveApp.Infrastructure;
using FindDriveApp.Models;
using FindDriveApp.Services.Interfaces;
using FindDriveApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FindDriveApp.ViewModels
{
    public class FindOrderViewModel : OrderBaseViewModel
    {
        public Command SaveCommand { get; set; }

        public FindOrderViewModel(IMessage message,
                                  IOrderService orderService, 
                                  IOrderReferencesService orderReferencesService
                                  
            )
            :base(message)
        {
            Title = "Поиск объявления";
            _message = message;
            _orderService = orderService;
            _orderReferencesService = orderReferencesService;
            
            Cities = new ObservableCollection<City>();
            SaveCommand = new Command(OnFindOrder, ValidateFind);

            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        public async override void OnAppearing()
        {
            try
            {
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

        private bool ValidateFind()
        {
            return SelectedFromCity != null && SelectedToCity != null;
        }

        private async void OnFindOrder()
        {
            try
            {
                var orderFilter = new OrderFilter
                {
                    FromCityId = SelectedFromCity.Id,
                    ToCityId = SelectedToCity.Id,
                    OrderTypeId = SelectedOrderType.Id,
                    DateOut = SelectedOutDate + SelectedOutTime,
                };
                await Shell.Current.GoToAsync($"//{nameof(OrdersPage)}?ordersFilter={JsonSerializer.Serialize(orderFilter)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
