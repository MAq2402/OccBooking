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
        public GetHallsQuery(Guid placeId, DateTime? date = null)
        {
            PlaceId = placeId;
            Date = date;
        }

        public Guid PlaceId { get; }
        public DateTime? Date { get; }
    }
}