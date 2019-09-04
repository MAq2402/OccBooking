using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Application.DTOs
{
    public class UserForCreationDto
    {
        public string FirstName { get; set; }
        public string LastName{ get; set; }
        public string Email { get; set; }
        public string PhoneNumber{ get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
