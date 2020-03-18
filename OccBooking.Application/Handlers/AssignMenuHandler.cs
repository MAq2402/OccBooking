using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Common.Hanlders;
using OccBooking.Domain.Entities;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.Handlers
{
    public class AssignMenuHandler : ICommandHandler<AssignMenuCommand>
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IPlaceRepository _placeRepository;

        public AssignMenuHandler(IMenuRepository menuRepository, IPlaceRepository placeRepository)
        {
            _menuRepository = menuRepository;
            _placeRepository = placeRepository;
        }

        public async Task<Result> HandleAsync(AssignMenuCommand command)
        {
            var place = await _placeRepository.GetPlaceAsync(command.PlaceId);
            if (place == null)
            {
                return Result.Fail("Place with given id does not exist");
            }

            var menu = new Menu(Guid.NewGuid(), command.Name, command.Type, command.CostPerPerson, place.Id);

            await _menuRepository.SaveAsync();

            return Result.Ok();
        }
    }
}