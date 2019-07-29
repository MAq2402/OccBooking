using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Exceptions
{
    public class ToSmallCapacityException : DomainException
    {
        public ToSmallCapacityException(string message) : base(message)
        {
        }
    }
}
