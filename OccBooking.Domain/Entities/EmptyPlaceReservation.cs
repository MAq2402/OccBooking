using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class EmptyPlaceReservation : Entity
    {
        public EmptyPlaceReservation(DateTime date)
        {
            Date = date;
        }

        private EmptyPlaceReservation()
        {

        }

        public DateTime Date { get; private set; }
        public Place Place { get; private set; }
    }
}
