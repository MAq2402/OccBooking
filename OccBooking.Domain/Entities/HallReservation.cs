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
        public Hall Hall { get; private set; }
        public ReservationRequest ReservationRequest { get; private set; }
        public DateTime Date { get; private set; }

        private HallReservation()
        {
        }

        private HallReservation(ReservationRequest reservationRequest)
        {
            ReservationRequest = reservationRequest;
            Date = ReservationRequest.DateTime;
        }

        private HallReservation(DateTime date)
        {
            Date = date;
        }

        public static HallReservation Create(ReservationRequest reservationRequest)
        {
            return new HallReservation(reservationRequest);
        }

        public static HallReservation CreateEmpty(DateTime date)
        {
            return new HallReservation(date);
        }
    }
}