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
    public class GetHallReservationsHandler : QueryHandler<GetHallReservationsQuery, IEnumerable<HallReservationDto>>
    {
        public GetHallReservationsHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<IEnumerable<HallReservationDto>>> HandleAsync(GetHallReservationsQuery query)
        {
            var hall = await _dbContext.Halls.Include(h => h.HallReservations)
                .ThenInclude(hr => hr.ReservationRequest)
                .FirstOrDefaultAsync(h => h.Id == query.HallId);

            if (hall == null)
            {
                return Result.Fail<IEnumerable<HallReservationDto>>("Could not find hall with given id");
            }

            var hallReservations = hall.HallReservations.Where(hr => hr.Date >= DateTime.Today.Date)
                .OrderBy(hr => hr.Date).Take(5);

            return Result.Ok(_mapper.Map<IEnumerable<HallReservationDto>>(hallReservations));
        }
    }
}