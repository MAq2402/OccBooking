using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Exceptions
{
    public class ReservationIsImpossibleException : DomainException
    {
        public ReservationIsImpossibleException(string message) : base(message)
        {
        }
    }
}
