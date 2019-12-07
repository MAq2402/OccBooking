using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using OccBooking.Application.Queries;
using OccBooking.Common.Dispatchers;

namespace OccBooking.Web.Controllers
{
    [Route("api/ingredients")]
    public class IngredientsController : BaseController
    {
        public IngredientsController(ICqrsDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetIngredientsAsync()
        {
            return FromCollection(await QueryAsync(new IngredientsQuery()));
        }
    }
}