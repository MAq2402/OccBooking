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
using OccBooking.Persistence.DbContexts;

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
                .FirstOrDefaultAsync(h => h.Id == query.HallId);

            if (hall == null)
            {
                return Result.Fail<IEnumerable<HallReservationDto>>("Could not find hall with given id");
            }

            var hallReservations = hall.HallReservations
                .Where(hr => hr.Date >= DateTime.Today.Date && hr.ReservationRequestId != null)
                .OrderBy(hr => hr.Date).Take(5);


            var result = _mapper.Map<IEnumerable<HallReservationDto>>(hallReservations);
            foreach (var item in result)
            {
                var reservationRequest =
                    await _dbContext.ReservationRequests.FirstOrDefaultAsync(r => r.Id == item.ReservationRequestId);
                _mapper.Map(reservationRequest, item);
            }

            return Result.Ok(result);
        }
    }
}