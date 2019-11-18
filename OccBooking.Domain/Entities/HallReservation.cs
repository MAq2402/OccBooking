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
        public HallReservation(ReservationRequest reservationRequest)
        {
            ReservationRequest = reservationRequest;
        }

        private HallReservation()
        {
        }

        public Hall Hall { get; private set; }
        public ReservationRequest ReservationRequest { get; private set; }

    }
}