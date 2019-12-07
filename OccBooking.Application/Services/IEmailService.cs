using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Application.Services
{
    public interface IEmailService
    {
        void Send(string message, Client receiver);
    }
}
