using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Common.Types
{
    public interface IEvent
    {
        Guid AggregateRootId { get; }
    }
}
