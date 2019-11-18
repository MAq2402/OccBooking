using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Application.DTOs
{
    public class PossibleJoinDto
    {
        public Guid HallId { get; set; }
        public bool IsPossible { get; set; }
    }
}
