using FindDriveApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FindDriveApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> InternalAuthAsync(User user);
        Task<bool> ExternalAuthAsync(string scheme);
        Task<User> RegistrationAsync(User user);
    }
}
