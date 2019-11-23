using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Domain.Entities;

namespace OccBooking.Persistance.Entities
{
    public class PlaceImage
    {
        public Guid Id { get; set; }
        public Guid PlaceId { get; set; }
        public Place Place { get; set; }
        public byte[] Content { get; set; }
    }
}
