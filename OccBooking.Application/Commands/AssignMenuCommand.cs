using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;
using OccBooking.Domain.Enums;

namespace OccBooking.Application.Commands
{
    public class AssignMenuCommand : ICommand
    {
        public AssignMenuCommand(Guid placeId, string name, MenuType type, decimal costPerPerson)
        {
            PlaceId = placeId;
            Name = name;
            Type = type;
            CostPerPerson = costPerPerson;
        }

        public Guid PlaceId { get; }
        public string Name { get; }
        public MenuType Type { get; }
        public decimal CostPerPerson { get; }
    }
}
