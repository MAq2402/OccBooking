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
    [Route("api/places/{placeId}/halls")]
    public class HallsController : BaseController
    {
        public HallsController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetHallsAsync(string placeId) =>
            FromCollection(await QueryAsync(new GetHallsQuery(new Guid(placeId))));

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddHallAsync(string placeId, [FromBody] HallForCreationDto dto) =>
            FromCreation(await CommandAsync(new AddHallCommand(dto.Name, dto.Capacity, new Guid(placeId))));


        [HttpPut]
        [Authorize]
        public async Task<IActionResult> MakeEmptyReservations(string placeId, [FromBody] IEnumerable<DateTimeOffset> dates) =>
            FromUpdate(await CommandAsync(new MakeEmptyReservationsCommand(dates, new Guid(placeId))));
    }
}