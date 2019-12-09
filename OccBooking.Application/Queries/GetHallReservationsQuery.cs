using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Application.DTOs;
using OccBooking.Common.Types;

namespace OccBooking.Application.Queries
{
    public class GetHallReservationsQuery : IQuery<IEnumerable<HallReservationDto>>
    {
        public GetHallReservationsQuery(Guid hallId)
        {
            HallId = hallId;
        }

        public Guid HallId { get; }
    }
}