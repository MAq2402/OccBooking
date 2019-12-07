﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OccBooking.Application.Services;
using OccBooking.Common.Types;
using OccBooking.Domain.Event;

namespace OccBooking.Application.EventHandlers
{
    public class ReservationRequestRejectedEventHandler : IEventHandler<ReservationRequestRejected>
    {
        private IEmailService _emailService;

        public ReservationRequestRejectedEventHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public Task HandleAsync(ReservationRequestRejected @event)
        {
            var emailMessage =
                $@"Twoja rezerwacja miejsca {@event.PlaceName} na dzien {@event.Date.ToString("dd/MM/yyyy")}
                została odrzucona. <h3>Podsumowanie</h3>";
            _emailService.Send(emailMessage, @event.Client);
            return Task.FromResult(0);
        }
    }
}