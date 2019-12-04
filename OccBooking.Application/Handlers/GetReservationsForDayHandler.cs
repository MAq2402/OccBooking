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
    public class GetReservationsForDayHandler : QueryHandler<GetReservationsForDayQuery, ReservationDto>
    {
        public GetReservationsForDayHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<ReservationDto>> HandleAsync(GetReservationsForDayQuery query)
        {
            var place = await _dbContext.Places
                .Include(p => p.EmptyReservations)
                .Include(p => p.Halls)
                .ThenInclude(h => h.HallReservations)
                .ThenInclude(h => h.ReservationRequest)
                .Include(p => p.EmptyReservations)
                .Include(p => p.Halls)
                .ThenInclude(h => h.HallReservations)
                .ThenInclude(h => h.Hall)
                .FirstOrDefaultAsync(p => p.Id == query.PlaceId);


            if (place == null)
            {
                return Result.Fail<ReservationDto>("Place with given id does not exist");
            }

            var hallReservations =
                _mapper.Map<IEnumerable<HallReservationDto>>(place.Halls.SelectMany(h => h.HallReservations)
                    .Where(hr => hr.Date == query.Date));
            var isEmpty = place.EmptyReservations.Any(r => r.Date == query.Date);

            return Result.Ok(new ReservationDto() {IsEmpty = isEmpty, HallReservations = hallReservations});
        }
    }
}