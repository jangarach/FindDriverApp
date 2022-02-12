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
    public class OrderReferencesService : IOrderReferencesService
    {
        private readonly HttpClient _httpClient;
        const string _address = "orderReferences";
        public OrderReferencesService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<City>> GetAllCities()
        {
            var response = await _httpClient.GetAsync($"{_address}/getAllCities/");

            response.EnsureSuccessStatusCode();
            var responseAsString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Deserialize<IEnumerable<City>>(responseAsString, options);
        }

        public async Task<IEnumerable<OrderType>> GetAllOrderTypes()
        {
            var response = await _httpClient.GetAsync($"{_address}/getAllOrderTypes/");

            response.EnsureSuccessStatusCode();
            var responseAsString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return JsonSerializer.Deserialize<IEnumerable<OrderType>>(responseAsString, options);
        }
    }
}
