using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OccBooking.Application.DTOs;
using OccBooking.Application.Handlers.Base;
using OccBooking.Application.Queries;
using OccBooking.Common.Hanlders;
using OccBooking.Persistence.DbContexts;
using OccBooking.Persistence.Entities;

namespace OccBooking.Application.Handlers
{
    public class GetUserHandler : QueryHandler<GetUserQuery, UserDto>
    {
        public GetUserHandler(OccBookingDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public override async Task<Result<UserDto>> HandleAsync(GetUserQuery query)
        {
            var user = await _dbContext.OccBookingUsers.Include(u => u.Owner)
                .FirstOrDefaultAsync(u => u.Id == query.Id);

            return user != null
                ? Result.Ok(_mapper.Map<UserDto>(user))
                : Result.Fail<UserDto>("User with given in does not exist");
        }
    }
}