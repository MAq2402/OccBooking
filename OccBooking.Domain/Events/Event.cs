using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Domain.Events
{
    public abstract class Event: IEvent
    {
        protected Event(Guid aggregateRootId)
        {
            AggregateRootId = aggregateRootId;
        }

        public Guid AggregateRootId { get; }
    }
}
