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
        protected IDispatcher _dispatcher;

        protected BaseController(IDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        protected async Task<Result> CommandAsync(ICommand command)
        {
            return await _dispatcher.DispatchAsync(command);
        }

        protected async Task<Result<T>> QueryAsync<T>(IQuery<T> query )
        { 
            return await _dispatcher.DispatchAsync(query);
        }
    }
}
