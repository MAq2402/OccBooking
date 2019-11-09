using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Application.DTOs;
using OccBooking.Common.Types;

namespace OccBooking.Application.Queries
{
    public class GetPlaceQuery : IQuery<PlaceDto>
    {
        public GetPlaceQuery(Guid placeId)
        {
            PlaceId = placeId;
        }

        public Guid PlaceId { get; }
    }
}