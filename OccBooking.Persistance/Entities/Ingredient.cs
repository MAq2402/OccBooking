using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Persistance.Entities
{
    public class Ingredient
    {
        public Ingredient(int id, string name)
        {
            Id = id;
            Name = name;
        }

        private Ingredient()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}