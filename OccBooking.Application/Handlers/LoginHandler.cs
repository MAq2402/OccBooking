using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Application.Contracts;
using OccBooking.Application.Queries;
using OccBooking.Common.Hanlders;

namespace OccBooking.Application.Handlers
{
    public class LoginHandler : IQueryHandler<LoginQuery, string>
    {
        private IAuthService _authService;

        public LoginHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<Result<string>> HandleAsync(LoginQuery query)
        {
            return await _authService.LoginAsync(query);
        }
    }
}
