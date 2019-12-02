using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OccBooking.Application.Commands;
using OccBooking.Application.DTOs;
using OccBooking.Application.Queries;
using OccBooking.Common.Dispatchers;

namespace OccBooking.Web.Controllers
{
    [Route("api")]
    public class HallsController : BaseController
    {
        public HallsController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        [Route("places/{placeId}/halls")]
        public async Task<IActionResult> GetHallsAsync(string placeId) =>
            FromCollection(await QueryAsync(new GetHallsQuery(new Guid(placeId))));

        [HttpPost]
        [Route("places/{placeId}/halls/filter")]
        public async Task<IActionResult> FilterHallsAsync(string placeId, FilterHallsDto dto) =>
            FromCollection(await QueryAsync(new GetHallsQuery(new Guid(placeId), dto.Date)));


        [HttpGet]
        [Route("halls/{id}")]
        public async Task<IActionResult> GetHallAsync(string id) =>
            FromSingle(await QueryAsync(new GetHallQuery(new Guid(id))));

        [HttpPut]
        [Route("halls/{id}/joins")]
        [Authorize]
        public async Task<IActionResult>
            UpdateHallJoinsAsync(string id, [FromBody] IEnumerable<PossibleJoinDto> joins) =>
            FromUpdate(await CommandAsync(new UpdateHallJoinsCommand(new Guid(id), joins)));

        [HttpPost]
        [Authorize]
        [Route("places/{placeId}/halls")]
        public async Task<IActionResult> AddHallAsync(string placeId, [FromBody] HallForCreationDto dto) =>
            FromCreation(await CommandAsync(new AddHallCommand(dto.Name, dto.Capacity, new Guid(placeId))));


        [HttpPost]
        [Authorize]
        [Route("places/{placeId}/halls/reserve")]
        public async Task<IActionResult> MakeEmptyPlaceReservationsAsync(string placeId,
            [FromBody] IEnumerable<DateTimeOffset> dates) =>
            FromCreation(await CommandAsync(new MakeEmptyReservationsCommand(dates.Select(d => d.LocalDateTime),
                new Guid(placeId))));

        [HttpPost]
        [Authorize]
        [Route("halls/{hallId}/reserve")]
        public async Task<IActionResult> MakeEmptyHallReservationsAsync(string hallId,
            [FromBody] IEnumerable<DateTimeOffset> dates) =>
            FromCreation(await CommandAsync(
                new MakeEmptyHallReservationsCommand(dates.Select(d => d.LocalDateTime), new Guid(hallId))));

        [HttpGet("halls/{id}/reservedDays")]
        [Authorize]
        public async Task<IActionResult> GetReservedDaysAsync(string id)
        {
            return FromCollection(await QueryAsync(new GetHallReservedDaysQuery(new Guid(id))));
        }
    }
}