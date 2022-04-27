using FindDriveApp.Models;
using FindDriveApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FindDriveApp.Services
{
    public class OrderService : IOrderService
    {
        private const string AuthHeaderName = "Authorization";
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> CreateOrder(Order order, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Remove(AuthHeaderName);
            _httpClient.DefaultRequestHeaders.Add(AuthHeaderName, "Bearer " + accessToken);

            var response = await _httpClient.PostAsync($"order/createOrder/",
                new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json"));
            return response;
        }

        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var response = await _httpClient.GetAsync($"order/getAllOrders/");
            response.EnsureSuccessStatusCode();
            var responseAsString = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(responseAsString))
                return default;

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Deserialize<IEnumerable<Order>>(responseAsString, options);
        }

        public async Task<IEnumerable<Order>> FindOrders(OrderFilter orderFilter)
        {
            var response = await _httpClient.PostAsync("order/getFindedOrders/",
                new StringContent(JsonSerializer.Serialize(orderFilter), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var responseAsString = await response.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(responseAsString))
                return default;

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Deserialize<IEnumerable<Order>>(responseAsString, options);
        }

        public async Task<HttpResponseMessage> UpdateOrder(Order order, string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Remove(AuthHeaderName);
            _httpClient.DefaultRequestHeaders.Add(AuthHeaderName, "Bearer " + accessToken);

            var response = await _httpClient.PostAsync($"order/updateOrder/",
                new StringContent(JsonSerializer.Serialize(order), Encoding.UTF8, "application/json"));
            return response;
        }
    }
}
 