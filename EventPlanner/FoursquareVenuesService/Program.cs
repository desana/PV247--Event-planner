using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoursquareVenuesService.Services;

namespace FoursquareVenuesService
{
    /// <summary>
    /// For testing only.
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            var foursquareService = new FoursquareService();
            var venueTask = foursquareService.GetVenueAsync("40a55d80f964a52020f31ee3");
            venueTask.Wait();
            var venue = venueTask.Result;
            Console.WriteLine(venue.Name);
            Console.ReadLine();
        }
    }
}
