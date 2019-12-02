﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Domain.Entities;

namespace OccBooking.Persistance.Repositories
{
    public interface IReservationRequestRepository : IRepository<ReservationRequest>
    {
        Task<ReservationRequest> GetReservationRequestAsync(Guid id);
    }
}
