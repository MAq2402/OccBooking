using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Application.Queries
{
    public class GetReservedDaysQuery : IQuery<IEnumerable<DateTime>>
    {
        public GetReservedDaysQuery(Guid placeId)
        {
            PlaceId = placeId;
        }

        public Guid PlaceId { get; }       
    }
}
