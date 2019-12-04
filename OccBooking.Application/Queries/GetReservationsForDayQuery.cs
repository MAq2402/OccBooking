using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Application.DTOs;
using OccBooking.Common.Types;

namespace OccBooking.Application.Queries
{
    public class GetReservationsForDayQuery : IQuery<ReservationDto>
    {
        public GetReservationsForDayQuery(Guid placeId, DateTime date)
        {
            PlaceId = placeId;
            Date = date;
        }

        public Guid PlaceId { get; }
        public DateTime Date { get; }
    }
}