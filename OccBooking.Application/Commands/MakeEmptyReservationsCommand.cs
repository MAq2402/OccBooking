using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Application.Commands
{
    public class MakeEmptyReservationsCommand : ICommand
    {
        public MakeEmptyReservationsCommand(IEnumerable<DateTimeOffset> dates, Guid placeId)
        {
            Dates = dates;
            PlaceId = placeId;
        }

        public Guid PlaceId { get; }
        public IEnumerable<DateTimeOffset> Dates { get; }
    }
}