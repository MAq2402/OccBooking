using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Enums;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Domain.Entities
{
    public class HallReservation : Entity
    {
        private string additionalOptions = string.Empty;
        public DateTime DateTime { get; private set; }
        public Client Client { get; private set; }
        public int AmountOfPeople { get; private set; }
        public Menu Menu { get; private set; }
        public decimal Cost { get; private set; }
        public OccasionType OccasionType { get; private set; }
        public Hall Hall { get; private set; }
        public PlaceAdditionalOptions AdditionalOptions
        {
            get { return (PlaceAdditionalOptions)additionalOptions; }
            set { additionalOptions = value; }
        }
        private HallReservation()
        {

        }
        public static HallReservation CreateFromReservationRequest(ReservationRequest reservationRequest, int amountOfPeople, decimal cost)
        {
            return new HallReservation()
            {
                Id = Guid.NewGuid(),
                DateTime = reservationRequest.DateTime,
                Client = reservationRequest.Client,
                AmountOfPeople = amountOfPeople,
                Menu = reservationRequest.Menu,
                Cost = cost,
                OccasionType = reservationRequest.OccasionType,
                AdditionalOptions = reservationRequest.AdditionalOptions
            };
        }

    }
}
