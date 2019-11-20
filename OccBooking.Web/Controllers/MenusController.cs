using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [Route("places/{placeId}/menus")]
        public async Task<IActionResult> GetMenus(string placeId)
        {
            return FromCollection(await QueryAsync(new GetMenusQuery(new Guid(placeId))));
        }

        [HttpPost]
        [Authorize]
        [Route("places/{placeId}/menus")]
        public async Task<IActionResult> CreateMenu(string placeId, [FromBody] MenuForCreationDto menu)
        {
            return FromCreation(await CommandAsync(new AssignMenuCommand(new Guid(placeId), menu.Name,
                menu.Type,
                menu.CostPerPerson)));
        }

        [HttpPost]
        [Authorize]
        [Route("menus/{id}/meals")]
        public async Task<IActionResult> AddMeal(string id, [FromBody] MealForCreationDto meal) =>
            FromCreation(await CommandAsync(new AddMealCommand(meal.Name, meal.Description, meal.Type, meal.Ingredients,
                new Guid(id))));
    }
}