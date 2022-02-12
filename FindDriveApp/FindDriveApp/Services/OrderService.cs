using FindDriveApp.Models;
using FindDriveApp.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FindDriveApp.Services
{
    public class OrderService : IOrderService
    {
        HttpClient _httpClient; 
        
        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateOrder(Order order)
        {
            var response = await _httpClient.PostAsync($"order/createOrder/",
                new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var response = await _httpClient.GetAsync($"order/getAllOrders/");

            response.EnsureSuccessStatusCode();
            var responseAsString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Deserialize<IEnumerable<Order>>(responseAsString, options);
        }
    }
}
 