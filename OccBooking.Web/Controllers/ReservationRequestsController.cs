using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OccBooking.Application.Commands;
using OccBooking.Application.DTOs;
using OccBooking.Common.Dispatchers;
using OccBooking.Domain.ValueObjects;

namespace OccBooking.Web.Controllers
{
    [Route("api")]
    public class ReservationRequestsController : BaseController
    {
        public ReservationRequestsController(IDispatcher dispatcher) : base(dispatcher)
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
    }
}