using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Common.Hanlders;
using OccBooking.Domain.Entities;
using OccBooking.Domain.ValueObjects;
using OccBooking.Persistance.Repositories;

namespace OccBooking.Application.Handlers
{
    public class MakeReservationRequestHandler : ICommandHandler<MakeReservationRequestCommand>
    {
        private IPlaceRepository _placeRepository;
        private IMenuRepository _menuRepository;

        public MakeReservationRequestHandler(IPlaceRepository placeRepository, IMenuRepository menuRepository)
        {
            _placeRepository = placeRepository;
            _menuRepository = menuRepository;
        }

        public async Task<Result> HandleAsync(MakeReservationRequestCommand command)
        {
            var place = await _placeRepository.GetPlaceAsync(command.PlaceId);

            if (place == null)
            {
                return Result.Fail("Place with given id does not exist");
            }

            var menuOrders = new List<MenuOrder>();
            foreach (var menuOrder in command.MenuOrders)
            {
                var menu = await _menuRepository.GetMenuAsync(menuOrder.Menu.Id);
                menuOrders.Add(new MenuOrder(menu, menuOrder.AmountOfPeople));
            }

            var optionsForReservationRequest = new List<PlaceAdditionalOption>();
            foreach (var optionDto in command.Options)
            {
                var option = new PlaceAdditionalOption(optionDto.Name, optionDto.Cost);
                optionsForReservationRequest.Add(option);
            }

            var reservationRequest = new ReservationRequest(Guid.NewGuid(), command.Date,
                new Client(command.ClientFirstName, command.ClientLastName, command.ClientEmail,
                    command.ClientPhoneNumber), command.OccasionType, optionsForReservationRequest, menuOrders);

            place.MakeReservationRequest(reservationRequest);

            await _placeRepository.SaveAsync();

            return Result.Ok();
        }
    }
}