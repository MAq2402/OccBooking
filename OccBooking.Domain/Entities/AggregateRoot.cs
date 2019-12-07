using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Domain.Entities
{
    public abstract class AggregateRoot : Entity
    {
        private static readonly List<IEvent> _events = new List<IEvent>();
        public static IEnumerable<IEvent> Events => _events;

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

        public static void ClearEvents()
        {
            _events.Clear();
        }
    }
}