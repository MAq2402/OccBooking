using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OccBooking.Common.Types
{
    public interface IEventHandler<T> where T : IEvent
    {
        Task HandleAsync(T @event);
    }
}
