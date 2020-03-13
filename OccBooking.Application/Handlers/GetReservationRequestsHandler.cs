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
using OccBooking.Domain.Entities;
using OccBooking.Persistence.DbContexts;

namespace OccBooking.Application.Handlers
{
    public class GetReservationRequestsHandler : QueryHandler<GetReservationRequestsQuery, IEnumerable<ReservationRequestDto>>
    {
        public GetReservationRequestsHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<IEnumerable<ReservationRequestDto>>> HandleAsync(
            GetReservationRequestsQuery query)
        {
            var places = _dbContext.Places.Where(p => p.OwnerId == query.OwnerId);
            var requests = await _dbContext.ReservationRequests.Where(r => places.Any(p => p.Id == r.PlaceId))
                .OrderByDescending(r => r.DateTime).ToListAsync();

            return Result.Ok(MapToReservationRequestsDto(requests, places));
        }

        private IEnumerable<ReservationRequestDto> MapToReservationRequestsDto(IEnumerable<ReservationRequest> reservationRequests, IEnumerable<Place> places)
        {
            var result = _mapper.Map<IEnumerable<ReservationRequestDto>>(reservationRequests);
            foreach (var request in result)
            {
                request.PlaceName = places.FirstOrDefault(p => p.Id == request.PlaceId).Name;
            }

            return result;
        }
    }
}