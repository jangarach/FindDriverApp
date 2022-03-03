using System;
using System.Collections.Generic;
using System.Text;

namespace FindDriveApp.Authenticate
{
    public interface IUserAuthenticate
    {
        void SimpleAuth();
        void GoogleAuth();
        void FaceBookAuth();
    }
}
