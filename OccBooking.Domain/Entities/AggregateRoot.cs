using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Domain.Entities
{
    public abstract class AggregateRoot : Entity
    {
        private static readonly List<IEvent> _events = new List<IEvent>();
        private List<IEvent> uncommittedEvents = new List<IEvent>();
        public static IEnumerable<IEvent> Events => _events;
        public IEnumerable<IEvent> UncommittedEvents => uncommittedEvents;


        protected AggregateRoot(Guid id) : base(id)
        {
        }

        protected AggregateRoot() : base()
        {
        }

        protected void AddEvent(IEvent @event)
        {
            _events.Add(@event);
        }

        protected void Raise(IEvent @event)
        {
            uncommittedEvents.Add(@event);
            On(@event);
        }

        public void On(IEvent @event)
        {
            ((dynamic)this).On((dynamic)@event);
        }

        public void ClearEvents()
        {
            uncommittedEvents.Clear();
        }
    }
}