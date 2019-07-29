using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Exceptions
{
    public class PartyIsNotAvaliableException : DomainException
    {
        public PartyIsNotAvaliableException(string message) : base(message)
        {
        }
    }
}
