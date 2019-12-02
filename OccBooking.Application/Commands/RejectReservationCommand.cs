using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Application.Commands
{
    public class RejectReservationCommand : ICommand
    {
        public RejectReservationCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}