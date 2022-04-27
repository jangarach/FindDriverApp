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
    public class UserOrdersViewModel : BaseViewModel
    {
        private readonly IMessage _message;
        private readonly IAuthStore _authStore;
        private readonly IOrderService _orderService;

        public Command LoadOrdersCommand { get; }
        public Command CloseOrderCommand { get; }
        public Command EditOrderCommand { get; }
        public ObservableCollection<Order> ActiveOrders { get; set; }
        public UserOrdersViewModel(IMessage message, IAuthStore authStore, IOrderService orderService)
        {
            _message = message;
            _authStore = authStore;
            _orderService = orderService;
            ActiveOrders = new ObservableCollection<Order>();
            LoadOrdersCommand = new Command(async () => await ExecuteLoadOrdersCommand());
            CloseOrderCommand = new Command<Order>(async (selectedOrder) => await OnCloseOrder(selectedOrder));
            EditOrderCommand = new Command<Order>(async (selectedOrder) => await OnEditOrder(selectedOrder));
        }

        public override void OnAppearing()
        {
            IsBusy = true;
        }

        async Task ExecuteLoadOrdersCommand()
        {
            IsBusy = true;

            try
            {
                ActiveOrders.Clear();
                if (_authStore.IsAuthorized == false)
                {
                    IsBusy = false;
                    UnAuthenticateHandler();
                    return;
                }

                _authStore.Refresh();

                var ordersFilter = new OrderFilter();
                ordersFilter.UserId = _authStore.UserId;
                ordersFilter.State = true;

                var orders = await _orderService.FindOrders(ordersFilter);
                if (orders == null)
                {
                    IsBusy = false;
                    _message.DisplayAlert("Результаты поиска", "У вас нет активных заказов!");
                    return;
                }

                foreach (var order in orders)
                {
                    ActiveOrders.Add(order);
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

        private async Task OnCloseOrder(Order selectedOrder)
        {
            try
            {
                _authStore.Refresh();
                selectedOrder.State = false;
                var response = await _orderService.UpdateOrder(selectedOrder, _authStore.AccessToken);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    UnAuthenticateHandler();
                    return;
                }
                else
                {
                    _message.LongToastAlert("Объявление успешно снято с объявлений");
                    LoadOrdersCommand.Execute(null);
                }
            }
            catch (Exception ex)
            {
                _message.DisplayAlert("Ошибка обновления записи", ex.Message);
            }
        }
        private async Task OnEditOrder(Order selectedOrder)
        {

        }

        private async void UnAuthenticateHandler()
        {
            _message.LongToastAlert("Необходимо авторизоваться");
            await Shell.Current.Navigation.PushModalAsync(new LoginPage());
        }
    }
}
