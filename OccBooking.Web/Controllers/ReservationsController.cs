using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OccBooking.Application.Queries;
using OccBooking.Common.Dispatchers;

namespace OccBooking.Web.Controllers
{
    [Route("api")]
    public class ReservationsController : BaseController
    {
        public ReservationsController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpPost("places/{placeId}/reservations")]
        [Authorize]
        public async Task<IActionResult> FindReservationsForDayAsync(string placeId, [FromBody] DateTimeOffset date) =>
            FromSingle(await QueryAsync(new GetReservationsForDayQuery(new Guid(placeId), date.LocalDateTime)));
    }
}