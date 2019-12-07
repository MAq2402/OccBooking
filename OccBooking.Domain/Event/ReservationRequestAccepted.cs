using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Domain.Event
{
    public class ReservationRequestAccepted : IEvent
    {
        public ReservationRequestAccepted(string placeName, DateTime date, Client client)
        {
            PlaceName = placeName;
            Date = date;
            Client = client;
        }

        public string PlaceName { get; }
        public DateTime Date { get; }
        public Client Client { get; }
    }
}
