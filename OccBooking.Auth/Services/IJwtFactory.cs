using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using OccBooking.Persistence.Entities;

namespace OccBooking.Auth.Services
{
    public interface IJwtFactory
    {
        string GenerateJwt(User user);
    }
}
