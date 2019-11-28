using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Application.Commands
{
    public class MakeEmptyReservationsCommand : ICommand
    {
        public MakeEmptyReservationsCommand(IEnumerable<DateTime> dates, Guid placeId)
        {
            Dates = dates;
            PlaceId = placeId;
        }

        public Guid PlaceId { get; }
        public IEnumerable<DateTime> Dates { get; }
    }
}