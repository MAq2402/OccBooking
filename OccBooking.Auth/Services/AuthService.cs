using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using OccBooking.Application.Commands;
using OccBooking.Application.Contracts;
using OccBooking.Application.Queries;
using OccBooking.Domain.Entities;
using OccBooking.Persistance.Entities;

namespace OccBooking.Auth.Services
{
    public class AuthService : IAuthService
    { 
        private UserManager<User> _userManager;
        private IJwtFactory _jwtFactory;

        public AuthService(UserManager<User> userManager, IJwtFactory jwtFactory)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
        }

        public async Task<Result> RegisterAsync(RegisterCommand command)
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

        public async Task<Result<string>> LoginAsync(LoginQuery query)
        {
            var user = await _userManager.FindByNameAsync(query.UserName);

            if (user == null)
            {
                return Result.Fail<string>("User with given user name does not exist");
            }

            if (!await _userManager.CheckPasswordAsync(user, query.Password))
            {
                return Result.Fail<string>("Wrong credentials");
            }

            var jwt = _jwtFactory.GenerateJwt(user, query.UserName);

            return Result.Ok(jwt);
        }
    }
}
