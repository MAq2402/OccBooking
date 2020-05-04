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
    public class EventPublisher : IEventPublisher
    {
        private IComponentContext _context;

        public EventPublisher(IComponentContext context)
        {
            _context = context;
        }

        public async Task PublishAsync<T>(T @event) where T : IEvent
        {
            var handlerType = typeof(IEventHandler<>)
                    .MakeGenericType(@event.GetType());

            dynamic handler = _context.Resolve(handlerType);

            await handler.HandleAsync((dynamic)@event);
        }
    }
}