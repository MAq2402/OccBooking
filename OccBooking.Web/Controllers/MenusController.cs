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
    [Route("api")]
    public class MenusController : BaseController
    {
        public MenusController(IDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpGet]
        [Route("{placeId}/menus")]
        public async Task<IActionResult> GetMenus(string placeId)
        {
            return FromCollection(await _dispatcher.DispatchAsync(new GetMenusQuery(new Guid(placeId))));
        }

        [HttpPost]
        [Route("{placeId}/menus")]
        public async Task<IActionResult> CreateMenu(string placeId, [FromBody] MenuForCreationDto menu)
        {
            return FromCreation(await _dispatcher.DispatchAsync(new AssignMenuCommand(new Guid(placeId), menu.Name,
                menu.Type,
                menu.CostPerPerson)));
        }

        [HttpGet]
        [Route("menus/ingredients")]
        public async Task<IActionResult> GetIngredientsAsync()
        {
            return FromCollection(await _dispatcher.DispatchAsync(new IngredientsQuery()));
        }
    }
}