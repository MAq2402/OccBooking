using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OccBooking.Application.Commands;
using OccBooking.Application.DTOs;
using OccBooking.Common.Dispatchers;

namespace OccBooking.Web.Controllers
{
    public class PlaceController : BaseController
    {
        public PlaceController(IDispatcher dispatcher) : base(dispatcher)
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlaceAsync(PlaceForCreationDto model)
        {
            var result = await _dispatcher.DispatchAsync(new CreatePlaceCommand(model.Name, model.HasRooms,
                model.CostPerPerson, model.Description));

            return result.IsSuccess ? (IActionResult) CreatedAtRoute(null, null) : BadRequest(result.Error);
        }
    }
}
