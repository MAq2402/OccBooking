using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OccBooking.Application.Services;
using OccBooking.Common.Types;
using OccBooking.Domain.Events;
using OccBooking.Persistance.DbContexts;

namespace OccBooking.Application.EventHandlers
{
    public class ReservationRequestRejectedEventHandler : IEventHandler<ReservationRequestRejected>
    {
        private IEmailService _emailService;
        private OccBookingDbContext _dbContext;

        public ReservationRequestRejectedEventHandler(IEmailService emailService, OccBookingDbContext dbContext)
        {
            _emailService = emailService;
            _dbContext = dbContext;
        }

        public async Task HandleAsync(ReservationRequestRejected @event)
        {
            var reservationRequest = await _dbContext.ReservationRequests.Include(r => r.Place)
                .FirstOrDefaultAsync(r => r.Id == @event.ReservationRequestId);

            var emailMessage =
                $@"Twoja rezerwacja miejsca {reservationRequest.Place.Name} na dzien {reservationRequest.DateTime:dd/MM/yyyy}
                została odrzucona. <h3>Podsumowanie</h3>";
            _emailService.Send(emailMessage, reservationRequest.Client);
        }
    }
}