using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using OccBooking.Application.DTOs;
using OccBooking.Application.Queries;
using OccBooking.Common.Hanlders;
using OccBooking.Persistance.DbContexts;
using OccBooking.Persistance.Entities;

namespace OccBooking.Application.Handlers
{
    public class GetUserHandler : IQueryHandler<GetUserQuery, UserDto>
    {
        private OccBookingDbContext _dbContext;
        private IMapper _mapper;

        public GetUserHandler(OccBookingDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> HandleAsync(GetUserQuery query)
        {
            var user = await _dbContext.OCcBookingUsers.Include(u => u.Owner)
                .FirstOrDefaultAsync(u => u.Id == query.Id);

            return user != null
                ? Result.Ok(_mapper.Map<UserDto>(user))
                : Result.Fail<UserDto>("User with given in does not exist");
        }
    }
}