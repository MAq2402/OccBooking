using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Application.DTOs
{
    public class HallDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public ICollection<HallJoinDto> Joins { get; set; } = new List<HallJoinDto>();
    }
}
