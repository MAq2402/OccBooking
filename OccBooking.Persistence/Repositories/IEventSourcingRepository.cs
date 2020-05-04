using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Domain.Entities;

namespace OccBooking.Persistence.Repositories
{
    public interface IEventSourcingRepository
    {
        Task SaveAsync(AggregateRoot aggregateRoot);
        Task<T> GetById<T>(Guid aggregateRootId) where T : AggregateRoot;
    }
}
