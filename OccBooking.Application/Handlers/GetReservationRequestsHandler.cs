using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OccBooking.Application.DTOs;
using OccBooking.Application.Handlers.Base;
using OccBooking.Application.Queries;
using OccBooking.Persistance.DbContexts;

namespace OccBooking.Application.Handlers
{
    public class GetReservationRequestsHandler : QueryHandler<GetReservationRequestsQuery, IEnumerable<ReservationRequestDto>>
    {
        public GetReservationRequestsHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<IEnumerable<ReservationRequestDto>>> HandleAsync(GetReservationRequestsQuery query)
        {
            var requests = await _dbContext.ReservationRequests.Include(r => r.Place).ThenInclude(p => p.Owner)
                .Where(r => r.Place.Owner.Id == query.OwnerId).OrderByDescending(r => r.DateTime).ToListAsync();

            return Result.Ok(_mapper.Map<IEnumerable<ReservationRequestDto>>(requests));
        }
    }
}
