using OccBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace OccBooking.Domain.Entities
{
    public class Owner : Entity
    {
        private List<Place> places = new List<Place>();
        public IEnumerable<Place> Places => places.AsReadOnly();

        public void AddPlace(Place place)
        {
            if(place == null)
            {
                throw new DomainException("Place has not been provided");
            }

            places.Add(place);
        }

        public void UpdatePlace(string name, 
            bool isShared, 
            bool hasRooms, 
            bool hasOwnFood, 
            decimal costForPerson, 
            decimal costForRent, 
            int capacity, 
            string description)
        {

        }

        
    }
}
