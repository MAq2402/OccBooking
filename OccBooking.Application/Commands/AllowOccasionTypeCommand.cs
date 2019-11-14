using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;
using OccBooking.Domain.Enums;

namespace OccBooking.Application.Commands
{
    public class AllowOccasionTypeCommand : ICommand
    {
        public AllowOccasionTypeCommand(Guid placeId, string type)
        {
            PlaceId = placeId;
            Type = type;
        }

        public Guid PlaceId { get; }
        public string Type { get; }
    }
}