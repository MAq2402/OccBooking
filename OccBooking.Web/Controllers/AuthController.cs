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
    public class AuthController : BaseController
    {
        public AuthController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForCreationDto model)
        {
            var command = new RegisterCommand(model.FirstName, model.LastName, model.Email, model.PhoneNumber,
                model.UserName, model.Password, model.ConfirmPassword);

            var result = await _dispatcher.DispatchAsync(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtRoute(null, null);
        }
    }
}