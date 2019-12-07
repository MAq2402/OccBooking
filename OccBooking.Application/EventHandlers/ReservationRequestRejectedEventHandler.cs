using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Common.Types;
using OccBooking.Domain.Event;

namespace OccBooking.Application.EventHandlers
{
    public class ReservationRequestRejectedEventHandler : IEventHandler<ReservationRequestRejected>
    {
        public Task HandleAsync(ReservationRequestRejected @event)
        {
            throw new NotImplementedException();
        }
    }
}
