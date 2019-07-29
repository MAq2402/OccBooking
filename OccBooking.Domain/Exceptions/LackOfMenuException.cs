using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Exceptions
{
    public class LackOfMenuException : DomainException
    {
        public LackOfMenuException(string message) : base(message)
        {
        }
    }
}
