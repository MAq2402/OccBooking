using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using OccBooking.Application.Commands;
using OccBooking.Auth;
using OccBooking.Auth.Services;
using OccBooking.Common.Hanlders;
using OccBooking.Domain.Entities;

namespace OccBooking.Application.Handlers
{
    public class RegisterHandler : ICommandHandler<RegisterCommand>
    {
        private UserManager<User> _userManager;
        private IJwtFactory _jwtFactory;

        public RegisterHandler(UserManager<User> userManager, IJwtFactory jwtFactory)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
        }

        public async Task<Result> HandleAsync(RegisterCommand command)
        {
            if (!command.Password.Equals(command.ConfirmPassword))
            {
                return Result.Fail("Passwords do not match");
            }

            var owner = new Owner(Guid.NewGuid(), command.FirstName, command.LastName, command.Email,
                command.PhoneNumber);

            var user = new User(owner, command.UserName);

            var result = await _userManager.CreateAsync(user, command.Password);

            return result.Succeeded ? Result.Ok() : Result.Fail("Account could not have been created");
        }
    }
}