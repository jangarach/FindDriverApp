using FindDriveApp.Infrastructure;
using FindDriveApp.Services;
using FindDriveApp.Services.Interfaces;
using FindDriveApp.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace FindDriveApp
{
    public static class Startup
    {
        private static IServiceProvider serviceProvider;
        public static void ConfigureServices()
        {
            var services = new ServiceCollection();

            //var baseAddress = new Uri("http://10.0.2.2:5000/api/");
            var baseAddress = new Uri("http://finddriver.kg:5000/api/");

            services.AddHttpClient("baseHttpClient", c =>
            {
                c.BaseAddress = baseAddress;
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddHttpClient<IOrderService, OrderService>("baseHttpClient");

            services.AddHttpClient<IOrderReferencesService, OrderReferencesService>("baseHttpClient");

            services.AddSingleton<IMessage, MessageAndroid>();
            services.AddSingleton<IAuthStore, AuthStore>();
            services.AddTransient<IUserService, UserService>();

            //TabBar viewmodels
            services.AddTransient<OrdersViewModel>();   //Общее
            services.AddTransient<NewOrderViewModel>(); //Добавить 
            services.AddTransient<UserProfileViewModel>();  //Профиль
            services.AddTransient<SettingsViewModel>(); //Настройки

            //other viewmodels
            services.AddTransient<LoginViewModel>();
            services.AddTransient<FindOrderViewModel>();
            services.AddTransient<OrderDetailViewModel>();
            services.AddTransient<UserRegistrationViewModel>();

            serviceProvider = services.BuildServiceProvider();
        }

        public static T Resolve<T>() => serviceProvider.GetService<T>();
    }
}
