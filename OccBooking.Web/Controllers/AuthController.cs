using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OccBooking.Application.Commands;
using OccBooking.Application.DTOs;
using OccBooking.Application.Queries;
using OccBooking.Auth;
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

            var result = await CommandAsync(command);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return await LoginAsync(new LoginCredentials() {Password = model.Password, UserName = model.UserName});
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCredentials model)
        {
            var query = new LoginQuery(model.UserName, model.Password);

            var result = await QueryAsync(query);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == Constants.UserId)?.Value;

            var result = await QueryAsync(new GetUserQuery(userId));

            return result.IsSuccess ? (IActionResult) Ok(result.Value) : NotFound(result.Error);
        }
    }
}