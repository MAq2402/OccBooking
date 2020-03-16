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
        private HallReservation(ReservationRequest reservationRequest)
        {
            ReservationRequestId = reservationRequest.Id;
            Date = reservationRequest.DateTime;
        }

        private HallReservation(DateTime date)
        {
            Date = date;
        }

        private HallReservation()
        {
        }

        public static HallReservation CreateEmpty(DateTime date)
        {
            return new HallReservation(date);
        }

        public static HallReservation CreateFromRequest(ReservationRequest reservationRequest)
        {
            return new HallReservation(reservationRequest);
        }

        public Hall Hall { get; private set; }
        public Guid? ReservationRequestId { get; private set; }
        public DateTime Date { get; private set; }
    }
}