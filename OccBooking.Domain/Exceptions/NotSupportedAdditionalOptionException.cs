using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Exceptions
{
    public class NotSupportedAdditionalOptionException : DomainException
    {
        public NotSupportedAdditionalOptionException(string message) : base(message)
        {
        }
    }
}
