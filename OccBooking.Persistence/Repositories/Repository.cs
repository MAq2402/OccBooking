using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Common.Dispatchers;
using OccBooking.Domain.Entities;
using OccBooking.Persistence.DbContexts;

namespace OccBooking.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : AggregateRoot
    {
        protected OccBookingDbContext _dbContext;
        private IEventPublisher eventPublisher;

        public Repository(OccBookingDbContext dbContext, IEventPublisher eventPublisher)
        {
            _dbContext = dbContext;
            this.eventPublisher = eventPublisher;
        }

        public async Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public async Task<bool> SaveAsync()
        {
            var result = await _dbContext.SaveChangesAsync();

            //var eventsToDispatch = AggregateRoot.Events.ToArray();
            //AggregateRoot.ClearEvents();
            //foreach (var @event in eventsToDispatch)
            //{
            //    await eventPublisher.PublishAsync(@event);
            //}
            return result > 0;
        }
    }
}