using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using OccBooking.Common.Dispatchers;
using OccBooking.Common.Types;

namespace OccBooking.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IDispatcher _dispatcher;

        protected BaseController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        protected async Task<Result> CommandAsync(ICommand command)
        {
            return await _dispatcher.DispatchAsync(command);
        }

        protected async Task<Result<T>> QueryAsync<T>(IQuery<T> query)
        {
            return await _dispatcher.DispatchAsync(query);
        }

        protected IActionResult FromCreation(Result result)
        {
            return result.IsSuccess ? (IActionResult) CreatedAtRoute(null, null) : BadRequest(result.Error);
        }

        protected IActionResult FromUpdate(Result result)
        {
            return result.IsSuccess ? (IActionResult) NoContent() : BadRequest(result.Error);
        }

        protected IActionResult FromSingle<T>(Result<T> result)
        {
            return result.IsSuccess ? (IActionResult) Ok(result.Value) : NotFound(result.Error);
        }

        protected IActionResult FromCollection<T>(Result<T> result)
        {
            return Ok(result.Value);
        }
    }
}