using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OccBooking.Application.Commands;
using OccBooking.Application.DTOs;
using OccBooking.Application.Queries;
using OccBooking.Common.Dispatchers;

namespace OccBooking.Web.Controllers
{
    public class AuthController : BaseController
    {
        public AuthController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] UserForCreationDto model)
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

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCredentials model)
        {
            var query = new LoginQuery(model.UserName, model.Password);

            var result = await _dispatcher.DispatchAsync(query);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }
    }
}