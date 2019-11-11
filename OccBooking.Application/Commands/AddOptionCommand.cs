using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Application.Commands
{
    public class AddOptionCommand : ICommand
    {
        public AddOptionCommand(string name, decimal cost, Guid placeId)
        {
            Name = name;
            Cost = cost;
            PlaceId = placeId;
        }

        public string Name { get; }
        public decimal Cost { get; }
        public Guid PlaceId { get; }
    }
}