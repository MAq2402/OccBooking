using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Application.DTOs;
using OccBooking.Common.Types;

namespace OccBooking.Application.Queries
{
    public class GetHallQuery : IQuery<ExtendedHallDto>
    {
        public GetHallQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
