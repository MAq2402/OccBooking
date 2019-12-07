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
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Web.Controllers
{
    [Route("api")]
    public class ReservationRequestsController : BaseController
    {
        public ReservationRequestsController(ICqrsDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpPost]
        [Route("places/{placeId}/reservationRequests")]
        public async Task<IActionResult> MakeReservationAsync([FromBody] MakeReservationRequestDto dto, string placeId)
        {
            return FromCreation(await CommandAsync(new MakeReservationRequestCommand(dto.ClientFirstName,
                dto.ClientLastName, dto.ClientEmail, dto.ClientPhoneNumber, dto.Date.LocalDateTime, dto.Options,
                OccasionType.Create(dto.OccasionType), dto.MenuOrders, new Guid(placeId))));
        }

        [HttpGet]
        [Authorize]
        [Route("owners/{ownerId}/reservationRequests")]
        public async Task<IActionResult> GetReservationRequestsAsync(string ownerId) =>
            FromCollection(await QueryAsync(new GetReservationRequestsQuery(new Guid(ownerId))));

        [HttpPut]
        [Authorize]
        [Route("reservationRequests/{id}/reject")]
        public async Task<IActionResult> RejectRequestAsync(string id) =>
            FromUpdate(await CommandAsync(new RejectReservationCommand(new Guid(id))));

        [HttpPut]
        [Authorize]
        [Route("reservationRequests/{id}/accept")]
        public async Task<IActionResult> AcceptRequestAsync(string id, [FromBody] IEnumerable<string> hallIds) =>
            FromUpdate(await CommandAsync(new AcceptReservationCommand(hallIds.Select(x => new Guid(x)), new Guid(id))));
    }
}