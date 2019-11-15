using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Application.DTOs;
using OccBooking.Common.Types;
using OccBooking.Domain.Entities;

namespace OccBooking.Application.Queries
{
    public class GetHallsQuery : IQuery<IEnumerable<HallDto>>
    {
        public GetHallsQuery(Guid placeId)
        {
            PlaceId = placeId;
        }

        public Guid PlaceId { get; private set; }
    }
}