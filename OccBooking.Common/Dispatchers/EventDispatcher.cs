using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using OccBooking.Common.Types;

namespace OccBooking.Common.Dispatchers
{
    public class EventDispatcher : IEventDispatcher
    {
        private IComponentContext _context;
        public EventDispatcher(IComponentContext context)
        {
            _context = context;
        }
        public async Task DispatchAsync<T>(params T[] events) where T : IEvent
        {
            foreach (var @event in events)
            {
                var handlerType = typeof(IEventHandler<>)
                    .MakeGenericType(@event.GetType());

                dynamic handler = _context.Resolve(handlerType);

                await handler.HandleAsync((dynamic)@event);
            }
        }
    }
}
