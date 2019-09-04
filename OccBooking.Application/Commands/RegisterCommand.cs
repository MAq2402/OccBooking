using System;
using System.Collections.Generic;
using System.Text;
using OccBooking.Common.Types;

namespace OccBooking.Application.Commands
{
    public class RegisterCommand : ICommand
    {
        public RegisterCommand(string firstName, 
            string lastName, 
            string email,
            string phoneNumber,
            string userName,
            string password,
            string confirmPassword)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            UserName = userName;
            Password = password;
            ConfirmPassword = confirmPassword;
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string ConfirmPassword { get; private set; }
    }
}
