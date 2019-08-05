using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Exceptions
{
    public class ReservationHasBeenAlreadyAnsweredException : DomainException
    {
        public ReservationHasBeenAlreadyAnsweredException(string message) : base(message)
        {
        }
    }
}
