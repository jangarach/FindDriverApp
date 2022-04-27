using FindDriveApp.Infrastructure;
using FindDriveApp.Models;
using FindDriveApp.Services.Interfaces;
using FindDriveApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FindDriveApp.ViewModels
{
    public class OrdersViewModel : OrderBaseViewModel, IQueryAttributable
    {
        public Command LoadOrdersCommand { get; }
        public Command FindOrderCommand { get; }
        public Command<Order> OrderTappedCommand { get; }
        private OrderFilter OrderFilter { get; set; }
        public OrdersViewModel(IMessage message, IOrderService orderService)
            :base(message)
        {
            Title = "Все объявления";
            _message = message;
            _orderService = orderService;

            Orders = new ObservableCollection<Order>();
            LoadOrdersCommand = new Command(async () => await ExecuteLoadOrdersCommand());
            FindOrderCommand = new Command(FindOrder);
            OrderTappedCommand = new Command<Order>(OnOrderSelected);
        }

        async Task ExecuteLoadOrdersCommand()
        {
            IsBusy = true;

            try
            {
                Orders.Clear();
                IEnumerable<Order> orders;

                if (OrderFilter != null)
                {
                    Title = "Результаты поиска";
                    orders = await _orderService.FindOrders(OrderFilter);
                    OrderFilter = null;
                }
                else
                {
                    Title = "Все объявления";
                    var orderFilter = new OrderFilter
                    {
                        State = true
                    };

                    orders = await _orderService.FindOrders(orderFilter);
                }

                if (orders == null)
                {
                    _message.DisplayAlert("Результаты поиска", "Не удалось найти объевления с текущими параметрами!");
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

        public override void OnAppearing()
        {
            IsBusy = true;
        }

        private async void FindOrder()
        {
            await Shell.Current.GoToAsync(nameof(FindOrderPage));
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            try
            {
                if (query.Count == 0 && query.ContainsKey("ordersFilter") == false)
                {
                    return;
                }

                var response = query["ordersFilter"];
                if (string.IsNullOrEmpty(response))
                {
                    return;
                }
                query.Clear();
                var options = new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                response = Uri.UnescapeDataString(response);
                this.OrderFilter = JsonSerializer.Deserialize<OrderFilter>(response, options);
            }
            catch(Exception ex)
            {
                _message.DisplayAlert("Ошибка в чтении аттрибута", ex.Message);
            }
        }

        private async void OnOrderSelected(Order selectedOrder)
        {
            if (selectedOrder == null)
                return; 

            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            await Shell.Current.GoToAsync($"{nameof(OrderDetailPage)}?order={JsonSerializer.Serialize(selectedOrder, options)}");
        }
    }
}
