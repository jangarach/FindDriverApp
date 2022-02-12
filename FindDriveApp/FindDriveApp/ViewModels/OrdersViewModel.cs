using FindDriveApp.Models;
using FindDriveApp.Services.Interfaces;
using FindDriveApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FindDriveApp.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private readonly IOrderService _orderService;
        public ObservableCollection<Order> Orders { get; set; }
        public Command LoadOrdersCommand { get; }
        public Command CommandFindOrder { get; }
        public OrdersViewModel(IOrderService orderService)
        {
            Title = "Все объявления";
            _orderService = orderService;
            Orders = new ObservableCollection<Order>();
            LoadOrdersCommand = new Command(async () => await ExecuteLoadOrdersCommand());
            CommandFindOrder = new Command(FindOrder);
        }

        async Task ExecuteLoadOrdersCommand()
        {
            IsBusy = true;

            try
            {
                Orders.Clear();

                var orders = await _orderService.GetAllOrders();
                foreach (var order in orders)
                {
                    Orders.Add(order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        public async void FindOrder()
        {
            await Shell.Current.GoToAsync(nameof(FindOrderPage));
        }
    }
}
