using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Application.DTOs
{
    public class MenuOrderDto
    {
        public MenuDto Menu { get; set; }
        public int AmountOfPeople { get; set; }
    }
}
