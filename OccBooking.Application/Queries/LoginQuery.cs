using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Application.Queries
{
    public class LoginQuery : IQuery<string>
    {
        public LoginQuery(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public string UserName { get; private set; }
        public string Password { get; private set; }
    }
}
