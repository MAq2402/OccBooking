using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OccBooking.Application.Commands;
using OccBooking.Application.DTOs;
using OccBooking.Application.Queries;
using OccBooking.Common.Dispatchers;

namespace OccBooking.Web.Controllers
{
    [Route("api")]
    public class PlacesController : BaseController
    {
        public PlacesController(ICqrsDispatcher dispatcher) : base(dispatcher)
        {
        }

        [HttpPost("places/filter")]
        public async Task<IActionResult> FilterPlacesAsync([FromBody] PlaceFilterDto dto) =>
            FromCollection(await QueryAsync(new GetPlacesQuery(dto)));

        [Authorize]
        [HttpGet("{ownerId}/places")]
        public async Task<IActionResult> GetOwnerPlacesAsync(string ownerId) =>
            FromCollection(await QueryAsync(new GetOwnerPlacesQuery(new Guid(ownerId))));


        [HttpGet("places/{placeId}")]
        public async Task<IActionResult> GetPlaceAsync(string placeId) =>
            FromSingle(await QueryAsync(new GetPlaceQuery(new Guid(placeId))));

        [Authorize]
        [HttpPost("{ownerId}/places")]
        public async Task<IActionResult> CreatePlaceAsync(string ownerId, PlaceForCreationDto model)
        {
            var command = new CreatePlaceCommand(model.Name, model.HasRooms, model.Description, model.Street,
                model.City, model.ZipCode, model.Province,
                new Guid(ownerId));

            var commandResult = await CommandAsync(command);

            if (commandResult.IsFailure)
            {
                return BadRequest(commandResult.Error);
            }

            var place = await QueryAsync(new GetPlaceQuery(command.Id));
            return CreatedAtRoute(null, place.Value);
        }

        [Authorize]
        [HttpPost("places/{placeId}/additionalOptions")]
        public async Task<IActionResult> AddOptionsForPlaceAsync(string placeId,
            [FromBody] AdditionalOptionForCreationDto dto) =>
            FromCreation(await CommandAsync(new AddOptionCommand(dto.Name, dto.Cost, new Guid(placeId))));

        [Authorize]
        [HttpPut("places/{placeId}/occasionTypes/{occasionType}/allow")]
        public async Task<IActionResult> AllowOccasionType(string placeId, string occasionType) =>
            FromUpdate(await CommandAsync(new AllowOccasionTypeCommand(new Guid(placeId), occasionType)));

        [Authorize]
        [HttpPut("places/{placeId}/occasionTypes/{occasionType}/disallow")]
        public async Task<IActionResult> DisallowOccasionType(string placeId, string occasionType) =>
            FromUpdate(await CommandAsync(new DisallowOccasionTypeCommand(new Guid(placeId), occasionType)));

        [HttpGet("places/{placeId}/reservedDays")]
        public async Task<IActionResult> GetReservedDays(string placeId) =>
            FromCollection(await QueryAsync(new GetReservedDaysQuery(new Guid(placeId))));

        [HttpPost("places/{placeId}/upload")]
        public async Task<IActionResult> UploadFile(string placeId)
        {
            if (!Request.Form.Files.Any())
            {
                return BadRequest();
            }

            var file = Request.Form.Files.First();

            return FromCreation(
                await CommandAsync(new UploadPlaceImageCommand(new Infrastructure.File(file), new Guid(placeId))));
        }

        [HttpPost("places/{placeId}/calculateMaxCapacity")]
        public async Task<IActionResult>
            CalculateMaxCapacityForDayAsync(string placeId, [FromBody] DateTimeOffset date) =>
            FromSingle(
                await QueryAsync(new CalculateMaximumCapacityForDayQuery(new Guid(placeId), date.LocalDateTime)));
    }
}