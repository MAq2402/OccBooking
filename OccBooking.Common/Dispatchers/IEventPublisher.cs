using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Common.Types;

namespace OccBooking.Common.Dispatchers
{
    public interface IEventPublisher
    {
        Task PublishAsync<T>(T @event) where T : IEvent;
    }
}
