using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Application.DTOs;
using OccBooking.Common.Hanlders;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Services;
using OccBooking.Domain.ValueObjects;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.Handlers
{
    public class MakeReservationRequestHandler : ICommandHandler<MakeReservationRequestCommand>
    {
        private readonly IPlaceRepository _placeRepository;
        private readonly IMenuRepository _menuRepository;
        private readonly IReservationRequestRepository _reservationRequestRepository;
        private readonly IReservationRequestService _reservationRequestService;
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public MakeReservationRequestHandler(IPlaceRepository placeRepository, IMenuRepository menuRepository,
            IReservationRequestRepository reservationRequestRepository,
            IReservationRequestService reservationRequestService, IEventSourcingRepository eventSourcingRepository)
        {
            _placeRepository = placeRepository;
            _menuRepository = menuRepository;
            _reservationRequestRepository = reservationRequestRepository;
            _reservationRequestService = reservationRequestService;
            _eventSourcingRepository = eventSourcingRepository;
        }

        public async Task<Result> HandleAsync(MakeReservationRequestCommand command)
        {
            var place = await _placeRepository.GetPlaceAsync(command.PlaceId);

            if (place == null)
            {
                return Result.Fail("Place with given id does not exist");
            }

            var menuOrders = await CreateMenuOrders(command.MenuOrders);

            var optionsForReservationRequest = MapPlaceAdditionalOptions(command.Options);

            var reservationRequest = ReservationRequest.MakeReservationRequest(Guid.NewGuid(), command.Date,
                new Client(command.ClientFirstName, command.ClientLastName, command.ClientEmail,
                    command.ClientPhoneNumber), command.OccasionType, optionsForReservationRequest, menuOrders,
                place.Id, place.Name);

            _reservationRequestService.ValidateMakeReservationRequest(place, reservationRequest,
                _placeRepository.IsPlaceConfigured);

            await _eventSourcingRepository.SaveAsync(reservationRequest);
            await _reservationRequestRepository.AddAsync(reservationRequest);
            await _reservationRequestRepository.SaveAsync();

            return Result.Ok();
        }

        private async Task<IEnumerable<MenuOrder>> CreateMenuOrders(IEnumerable<MenuOrderDto> menuOrders)
        {
            var result = new List<MenuOrder>();
            foreach (var menuOrder in menuOrders)
            {
                var menu = await _menuRepository.GetMenuAsync(menuOrder.Menu.Id);
                result.Add(new MenuOrder(menu, menuOrder.AmountOfPeople));
            }

            return result;
        }

        private IEnumerable<PlaceAdditionalOption> MapPlaceAdditionalOptions(
            IEnumerable<AdditionalOptionDto> optionsDtos)
        {
            var optionsForReservationRequest = new List<PlaceAdditionalOption>();
            foreach (var optionDto in optionsDtos)
            {
                var option = new PlaceAdditionalOption(optionDto.Name, optionDto.Cost);
                optionsForReservationRequest.Add(option);
            }

            return optionsForReservationRequest;
        }
    }
}