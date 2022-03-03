using FindDriveApp.Infrastructure;
using FindDriveApp.Models;
using FindDriveApp.Services;
using FindDriveApp.Services.Interfaces;
using FindDriveApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FindDriveApp.ViewModels
{
    public class NewOrderViewModel : BaseViewModel
    {
        private readonly IMessage _message;
        private readonly IOrderService _orderService;
        private readonly IOrderReferencesService _orderReferencesService;


        private City _selectedFromCity;
        private City _selectedToCity;
        private OrderType _selectedOrderType;
        private DateTime _selectedOutDate = DateTime.Now;
        private TimeSpan _selectedOutTime = DateTime.Now.TimeOfDay;
        public ObservableCollection<City> Cities { get; set; }
        public ObservableCollection<OrderType> OrderTypes { get; set; }

        public Command CancelCommand { get; set; }
        public Command SaveCommand { get; set; }

        public NewOrderViewModel(IOrderService orderService,
                                 IOrderReferencesService orderReferencesService,
                                 IMessage message)
        {
            _message = message;
            _orderService = orderService;
            _orderReferencesService = orderReferencesService;

            Cities = new ObservableCollection<City>();
            OrderTypes = new ObservableCollection<OrderType>();

            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);

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


        public City SelectedFromCity
        {
            get => _selectedFromCity;
            set => SetProperty(ref _selectedFromCity, value);
        }

        public City SelectedToCity
        {
            get => _selectedToCity;
            set => SetProperty(ref _selectedToCity, value);
        }

        public OrderType SelectedOrderType
        {
            get => _selectedOrderType;
            set => SetProperty(ref _selectedOrderType, value);
        }

        public DateTime SelectedOutDate
        {
            get => _selectedOutDate;
            set => SetProperty(ref _selectedOutDate, value);
        }

        public TimeSpan SelectedOutTime
        {
            get => _selectedOutTime;
            set => SetProperty(ref _selectedOutTime, value);
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
                var order = new Order()
                {
                    FromCityId = SelectedFromCity.Id,
                    ToCityId = SelectedToCity.Id,
                    DateOut = SelectedOutDate + SelectedOutTime,
                    OrderTypeId = SelectedOrderType.Id,
                };
                await _orderService.CreateOrder(order);
                _message.LongToastAlert("Запись успешно добавлена!");
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
