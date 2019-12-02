using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Application.DTOs;
using OccBooking.Common.Types;

namespace OccBooking.Application.Queries
{
    public class GetReservationRequestsQuery : IQuery<IEnumerable<ReservationRequestDto>>
    {
        public GetReservationRequestsQuery(Guid ownerId)
        {
            OwnerId = ownerId;
        }

        public Guid OwnerId { get; }
    }
}