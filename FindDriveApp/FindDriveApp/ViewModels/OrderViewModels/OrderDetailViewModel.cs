using FindDriveApp.Infrastructure;
using FindDriveApp.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using Xamarin.Forms;

namespace FindDriveApp.ViewModels
{
    public class OrderDetailViewModel : OrderBaseViewModel, IQueryAttributable
    {
        public OrderDetailViewModel(IMessage message)
            :base(message)
        {

        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            if (query.Count == 0 || query.ContainsKey("order") == false)
                return;

            var response = query["order"];
            if (string.IsNullOrEmpty(response))
                return;

            response = Uri.UnescapeDataString(response);

            var options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response = Uri.UnescapeDataString(response);
            this.Order = JsonSerializer.Deserialize<Order>(response, options);
        }
    }
}
