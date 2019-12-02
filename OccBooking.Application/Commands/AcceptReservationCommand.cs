using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Application.Commands
{
    public class AcceptReservationCommand : ICommand
    {
        public AcceptReservationCommand(IEnumerable<Guid> hallIds, Guid reservationId)
        {
            HallIds = hallIds;
            ReservationId = reservationId;
        }

        public IEnumerable<Guid> HallIds { get; }
        public Guid ReservationId { get; }
    }
}