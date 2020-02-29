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

namespace OccBooking.Application.EventHandlers
{
    public class ReservationRequestAcceptedEventHandler : IEventHandler<ReservationRequestAccepted>
    {
        private IEmailService _emailService;
        private OccBookingDbContext _dbContext;

        public ReservationRequestAcceptedEventHandler(IEmailService emailService, OccBookingDbContext dbContext)
        {
            _emailService = emailService;
            _dbContext = dbContext;
        }

        public async Task HandleAsync(ReservationRequestAccepted @event)
        {

            MakeHallReservations(request, halls);

            RejectReservationsRequestsIfNotEnoughCapacity();

            var reservationRequest = await _dbContext.ReservationRequests.Include(r => r.Place)
                .FirstOrDefaultAsync(r => r.Id == @event.ReservationRequestId);

            var emailMessage =
                $@"Twoja rezerwacja miejsca {reservationRequest.Place.Name} na dzien {reservationRequest.DateTime:dd/MM/yyyy}
                została zaakcepotwana. <h3>Podsumowanie</h3>";
            _emailService.Send(emailMessage, reservationRequest.Client);
        }

        private void MakeHallReservations(ReservationRequest request, IEnumerable<Hall> halls)
        {
            foreach (var hall in halls)
            {
                hall.MakeReservation(request);
            }
        }

        public void RejectReservationsRequestsIfNotEnoughCapacity()
        {
            var requestsToReject = ReservationRequests.Where(r =>
                !r.IsAnswered && !DoHallsHaveEnoughCapacity(r.DateTime, r.AmountOfPeople));

            foreach (var requestToReject in requestsToReject)
            {
                requestToReject.Reject();
            }
        }
    }
}