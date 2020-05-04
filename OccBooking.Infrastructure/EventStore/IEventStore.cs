using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Common.Types;

namespace OccBooking.Infrastructure.EventStore
{
    public interface IEventStore
    {
        Task WriteEventAsync(IEvent @event);
        Task<IEnumerable<IEvent>> ReadEventsAsync(Guid aggregateRootId);
    }
}
