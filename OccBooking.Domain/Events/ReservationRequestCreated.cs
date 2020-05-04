using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;
using OccBooking.Domain.Entities;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Domain.Events
{
    public class ReservationRequestCreated : Event
    {
        public ReservationRequestCreated(Guid reservationRequestId,
            DateTime dateTime,
            Client client,
            OccasionType occasionType,
            IEnumerable<PlaceAdditionalOption> additionalOptions,
            IEnumerable<MenuOrder> menuOrders,
            Guid placeId) : base(reservationRequestId)
        {
            DateTime = dateTime;
            Client = client;
            MenuOrders = menuOrders;
            OccasionType = occasionType;
            AdditionalOptions = new PlaceAdditionalOptions(additionalOptions);
            PlaceId = placeId;
        }

        public DateTime DateTime { get; }
        public Client Client { get; }
        public OccasionType OccasionType { get; }
        public PlaceAdditionalOptions AdditionalOptions { get; }
        public IEnumerable<MenuOrder> MenuOrders { get; }
        public Guid PlaceId { get; }
    }
}
