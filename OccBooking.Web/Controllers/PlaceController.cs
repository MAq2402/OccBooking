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
    [Route("api")]
    public class PlaceController : BaseController
    { 
    
        public PlaceController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpPost("{ownerId}/place")]
        public async Task<IActionResult> CreatePlaceAsync(string ownerId, PlaceForCreationDto model)
        {
            var result = await _dispatcher.DispatchAsync(new CreatePlaceCommand(model.Name, model.HasRooms,
                model.CostPerPerson, model.Description, model.Street, model.City, model.ZipCode, model.Province,
                new Guid(ownerId)));

            return result.IsSuccess ? (IActionResult) CreatedAtRoute(null, null) : BadRequest(result.Error);
        }
    }
}