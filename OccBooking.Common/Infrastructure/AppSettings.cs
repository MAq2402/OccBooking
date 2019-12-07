using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Common.Infrastructure
{
    public class AppSettings
    {
        public AppSettings(string emailAddress, string emailPassword, string emailName, string smtpHost, int smtpPort)
        {
            EmailAddress = emailAddress;
            EmailPassword = emailPassword;
            EmailName = emailName;
            SmtpHost = smtpHost;
            SmtpPort = smtpPort;
        }

        public string EmailAddress { get; }
        public string EmailPassword { get; }
        public string EmailName { get; }
        public string SmtpHost { get; }
        public int SmtpPort { get; }
    }
}