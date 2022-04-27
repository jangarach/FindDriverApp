using FindDriveApp.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FindDriveApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<IEnumerable<Order>> FindOrders(OrderFilter orderFilter);
        Task<HttpResponseMessage> CreateOrder(Order order, string accessToken);
        Task<HttpResponseMessage> UpdateOrder(Order order, string accessToken);
    }
}
