using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoursquareVenuesService.Entities;

namespace FoursquareVenuesService.Services
{
    public class FoursquareService : IFoursquareService
    {
        public IEnumerable<Venue> SearchVenues(string query, string city, int numberOfResults)
        {
            throw new NotImplementedException();
        }

        public Venue GetVenue(int id)
        {
            throw new NotImplementedException();
        }
    }
}
