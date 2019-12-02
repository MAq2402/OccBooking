using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Application.Commands
{
    public class MakeEmptyHallReservationsCommand : ICommand
    {
        public MakeEmptyHallReservationsCommand(IEnumerable<DateTime> dates, Guid id)
        {
            Dates = dates;
            HallId = id;
        }

        public Guid HallId { get; }
        public IEnumerable<DateTime> Dates { get; }
    }
}
