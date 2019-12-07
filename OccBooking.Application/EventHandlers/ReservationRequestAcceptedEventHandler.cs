using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Application.Services;
using OccBooking.Common.Types;
using OccBooking.Domain.Event;

namespace OccBooking.Application.EventHandlers
{
    public class ReservationRequestAcceptedEventHandler : IEventHandler<ReservationRequestAccepted>
    {
        private IEmailService _emailService;

        public ReservationRequestAcceptedEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public Task HandleAsync(ReservationRequestAccepted @event)
        {
            var emailMessage =
                $@"Twoja rezerwacja miejsca {@event.PlaceName} na dzien {@event.Date.ToString("dd/MM/yyyy")}
                została zaakcepotwana. <h3>Podsumowanie</h3>";
            _emailService.Send(emailMessage, @event.Client);
            return Task.FromResult(0);
        }
    }
}
