using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Common.Dispatchers;
using OccBooking.Domain.Entities;
using OccBooking.Infrastructure.EventStore;
using Remotion.Linq.Clauses;

namespace OccBooking.Persistence.Repositories
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private IEventStore _eventStore;
        private IEventPublisher _eventPublisher;

        public EventSourcingRepository(IEventStore eventStore, IEventPublisher eventPublisher)
        {
            _eventStore = eventStore;
            _eventPublisher = eventPublisher;
        }

        public async Task SaveAsync(AggregateRoot aggregateRoot)
        {
            foreach (var @event in aggregateRoot.UncommittedEvents)
            {
                await _eventStore.WriteEventAsync(@event);
                await _eventPublisher.PublishAsync(@event);
            }

            aggregateRoot.ClearEvents();
        }

        public async Task<T> GetById<T>(Guid aggregateRootId) where T : AggregateRoot
        {
            var events = await _eventStore.ReadEventsAsync(aggregateRootId);
            var result = CreateEmptyAggregateRoot<T>();

            foreach (var @event in events)
            {
                result.On(@event);
            }

            return result;
        }

        private T CreateEmptyAggregateRoot<T>() where T : AggregateRoot
        {
            return Activator.CreateInstance(default(T).GetType(), true) as T;
        }
    }
}
