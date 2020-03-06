using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OccBooking.Application.Services;
using OccBooking.Common.Types;
using OccBooking.Domain.Entities;
using OccBooking.Domain.Events;
using OccBooking.Persistence.DbContexts;
using OccBooking.Persistence.Repositories;

namespace OccBooking.Application.EventHandlers
{
    public class ReservationRequestAcceptedEventHandler : IEventHandler<ReservationRequestAccepted>
    {
        private readonly IEmailService _emailService;
        private readonly OccBookingDbContext _dbContext;
        private readonly IHallRepository _hallRepository;
        private IReservationRequestRepository _reservationRequestRepository;

        public ReservationRequestAcceptedEventHandler(IEmailService emailService, OccBookingDbContext dbContext,
            IHallRepository hallRepository, IReservationRequestRepository reservationRequestRepository)
        {
            _emailService = emailService;
            _dbContext = dbContext;
            _hallRepository = hallRepository;
            _reservationRequestRepository = reservationRequestRepository;
        }

        public async Task HandleAsync(ReservationRequestAccepted @event)
        {
            var reservationRequest =
                await _reservationRequestRepository.GetReservationRequestAsync(@event.ReservationRequestId);

            await MakeHallsReservationsAsync(reservationRequest, @event.HallIds);

            await _hallRepository.SaveAsync();

            await RejectImpossibleReservationsRequestsAsync(@event.PlaceId, reservationRequest.DateTime);

            await _reservationRequestRepository.SaveAsync();

            await SendEmailAsync(@event);
        }

        private async Task MakeHallsReservationsAsync(ReservationRequest request, IEnumerable<Guid> hallIds)
        {
            foreach (var hallId in hallIds)
            {
                var hall = await _hallRepository.GetHallAsync(hallId);
                hall.MakeReservation(request);
            }
        }

        private async Task RejectImpossibleReservationsRequestsAsync(Guid placeId, DateTime dateTime)
        {
            var requestsToReject = await _reservationRequestRepository.GetImpossibleReservationRequestsAsync(placeId, dateTime);

            foreach (var requestToReject in requestsToReject)
            {
                requestToReject.Reject();
            }
        }

        private async Task SendEmailAsync(ReservationRequestAccepted @event)
        {
            var reservationRequest = await _dbContext.ReservationRequests.Include(r => r.Place)
                .FirstOrDefaultAsync(r => r.Id == @event.ReservationRequestId);

            var emailMessage =
                $@"Twoja rezerwacja miejsca {reservationRequest.Place.Name} na dzien {reservationRequest.DateTime:dd/MM/yyyy}
                została zaakcepotwana. <h3>Podsumowanie</h3>";
            _emailService.Send(emailMessage, reservationRequest.Client);
        }
    }
}