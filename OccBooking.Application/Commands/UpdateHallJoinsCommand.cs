using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Application.DTOs;
using OccBooking.Common.Types;

namespace OccBooking.Application.Commands
{
    public class UpdateHallJoinsCommand : ICommand
    {
        public UpdateHallJoinsCommand(Guid hallId, IEnumerable<PossibleJoinDto> joins)
        {
            HallId = hallId;
            Joins = joins;
        }

        public Guid HallId { get; }
        public IEnumerable<PossibleJoinDto> Joins { get; }
    }
}