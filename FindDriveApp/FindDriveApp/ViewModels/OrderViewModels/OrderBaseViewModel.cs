using FindDriveApp.Infrastructure;
using FindDriveApp.Models;
using FindDriveApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FindDriveApp.ViewModels
{
    public class OrderBaseViewModel : BaseViewModel
    {
        protected IMessage _message;
        protected IOrderService _orderService;
        protected IOrderReferencesService _orderReferencesService;

        private Order _order;
        private City _selectedFromCity;
        private City _selectedToCity;
        private OrderType _selectedOrderType;
        private DateTime _selectedOutDate = DateTime.Now;
        private TimeSpan _selectedOutTime = DateTime.Now.TimeOfDay;
        private string _phoneNumber;
        private string _comment;
        private int _passengersCount = 1;

        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
        public ObservableCollection<City> Cities { get; set; } = new ObservableCollection<City>();
        public ObservableCollection<OrderType> OrderTypes { get; set; } = new ObservableCollection<OrderType>();

        public OrderBaseViewModel(IMessage message)
        {
            _message = message;
        }

        public Order Order
        {
            get => _order;
            set => SetProperty(ref _order, value);
        }

        #region Order properties
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

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public string Comment
        {
            get => _comment;
            set => SetProperty(ref _comment, value);
        }

        public int PassengersCount
        {
            get => _passengersCount;
            set => SetProperty(ref _passengersCount, value);
        }

        #endregion 

        protected virtual void LoadOrder(int id)
        {

        }

        protected virtual void LoadOrder(Order order)
        {

        }

        protected virtual async void LoadOrdersByFilter(OrderFilter orderFilter)
        {
            IsBusy = true;

            try
            {
                Orders.Clear();
                IEnumerable<Order> orders;

                if (orderFilter == null)
                {
                    throw new ArgumentNullException(nameof(orderFilter));
                }

                if (_orderService == null)
                {
                    throw new NullReferenceException(nameof(_orderService));
                }

                orders = await _orderService.FindOrders(orderFilter);

                if (orders == null)
                {
                    IsBusy = false;
                    _message.DisplayAlert("Результаты поиска", "Не удалось найти объявления с текущими параметрами!");
                    return;
                }

                foreach (var order in orders)
                {
                    Orders.Add(order);
                }
            }
            catch (Exception ex)
            {
                _message.DisplayAlert("Ошибка при загрузке", ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected virtual async void LoadOrderReferences()
        {
            IsBusy = true;
            try
            {
                if (_orderService == null)
                {
                    throw new NullReferenceException(nameof(_orderService));
                }

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
                _message.DisplayAlert("Ошибка при загрузке справочников", ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
