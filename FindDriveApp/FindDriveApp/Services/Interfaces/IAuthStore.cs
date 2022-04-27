using System;
using System.Collections.Generic;
using System.Text;

namespace FindDriveApp.Services.Interfaces
{
    public interface IAuthStore
    {
        Guid UserId { get; }
        string AccessToken { get; }
        DateTime ExpiresToken { get; }
        bool IsAuthorized { get; }
        void Refresh();
        void SetStore(string userId, string accessToken, string expiresToken);
    }
}
