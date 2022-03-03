using System;
using System.Collections.Generic;
using System.Text;

namespace FindDriveApp.Infrastructure
{
    public interface IMessage
    {
        void LongToastAlert(string message);
        void ShortToastAlert(string message);
        void DisplayAlert(string title, string message);
    }
}
