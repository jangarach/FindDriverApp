using FindDriveApp.Infrastructure;
using FindDriveApp.Services;
using FindDriveApp.Services.Interfaces;
using FindDriveApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace FindDriveApp
{
    public static class Startup
    {
        private static IServiceProvider serviceProvider;
        public static void ConfigureServices()
        {
            var services = new ServiceCollection();

            var baseAddress = new Uri("http://10.0.2.2:5000/api/"); 

            //add services
            services.AddHttpClient<IOrderService, OrderService>(c =>
            {
                c.BaseAddress = baseAddress;
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddHttpClient<IOrderReferencesService, OrderReferencesService>(c =>
            {
                c.BaseAddress = baseAddress;
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddSingleton<IMessage, MessageAndroid>();

            services.AddTransient<OrdersViewModel>();
            services.AddTransient<NewOrderViewModel>();
            services.AddTransient<FindOrderViewModel>();
            serviceProvider = services.BuildServiceProvider();
        }

        public static T Resolve<T>() => serviceProvider.GetService<T>();
    }
}
