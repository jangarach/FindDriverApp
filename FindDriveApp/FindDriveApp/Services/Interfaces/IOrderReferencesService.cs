using FindDriveApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindDriveApp.Services.Interfaces
{
    public interface IOrderReferencesService
    {
        Task<IEnumerable<City>> GetAllCities();
        Task<IEnumerable<OrderType>> GetAllOrderTypes();
    }
}
