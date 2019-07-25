using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Meal
    {
        private List<string> ingredients = new List<string>();
        public IEnumerable<string> Ingredients => ingredients.AsReadOnly();
    }
}
