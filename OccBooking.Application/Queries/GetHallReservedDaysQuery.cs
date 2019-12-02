using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Application.Queries
{
    public class GetHallReservedDaysQuery : IQuery<IEnumerable<DateTime>>
    {
        public GetHallReservedDaysQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}