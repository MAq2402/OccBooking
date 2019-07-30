using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.ValueObjects
{
    public class Localization : ValueObject
    {
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
