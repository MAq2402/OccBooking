using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Application.Queries
{
    public class CalculateMaximumCapacityForDayQuery : IQuery<int>
    {
        public CalculateMaximumCapacityForDayQuery(Guid placeId, DateTime date)
        {
            PlaceId = placeId;
            Date = date;
        }

        public Guid PlaceId { get; }
        public DateTime Date { get; }
    }
}