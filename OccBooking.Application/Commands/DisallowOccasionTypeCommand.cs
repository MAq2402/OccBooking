using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;
using OccBooking.Domain.Enums;

namespace OccBooking.Application.Commands
{
    public class DisallowOccasionTypeCommand : ICommand
    {
        public DisallowOccasionTypeCommand(Guid placeId, OccasionType type)
        {
            PlaceId = placeId;
            Type = type;
        }

        public Guid PlaceId { get; }
        public OccasionType Type { get; }
    }
}
