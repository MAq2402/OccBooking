using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Application.Services;
using OccBooking.Common.Types;
using OccBooking.Domain.Event;

namespace OccBooking.Application.EventHandlers
{
    public class ReservationRequestCreatedEventHandler : IEventHandler<ReservationRequestCreated>
    {
        private IEmailService _emailService;

        public ReservationRequestCreatedEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public Task HandleAsync(ReservationRequestCreated @event)
        {
            var emailMessage =
                $@"Twoja rezerwacja miejsca {@event.PlaceName} na dzien {@event.Date.ToString("dd/MM/yyyy")}
                została utworzona pomyślnie. <h3>Podsumowanie</h3>";
            _emailService.Send(emailMessage, @event.Client);
            return Task.FromResult(0);
        }
    }
}