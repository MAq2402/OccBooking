using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Domain.Entities;

namespace OccBooking.Persistance.Repositories
{
    public interface IRepository<T> where T: AggregateRoot
    {
        Task AddAsync(T entity);
        Task<bool> SaveAsync();
    }
}
