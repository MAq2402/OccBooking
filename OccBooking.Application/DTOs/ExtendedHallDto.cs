using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Application.DTOs
{
    public class ExtendedHallDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid PlaceId { get; set; }
        public ICollection<PossibleJoinDto> Joins { get; set; } = new List<PossibleJoinDto>();
    }
}