using FindDriveApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindDriveApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task CreateOrder(Order order);
    }
}
