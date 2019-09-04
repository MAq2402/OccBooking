using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using OccBooking.Application.Commands;
using OccBooking.Application.DTOs;
using OccBooking.Application.Queries;

namespace OccBooking.Application.Contracts
{
    public interface IAuthService
    {
        Task<Result> RegisterAsync(RegisterCommand command);
        Task<Result<string>> LoginAsync(LoginQuery query);
    }
}
