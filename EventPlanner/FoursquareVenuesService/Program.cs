using System;
using System.Collections.Generic;
using System.Configuration;
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
            var foursquareService = new FoursquareService(new FoursquareOptions
            {
                ClientId = ConfigurationManager.AppSettings["clientId"],
                ClientSecret = ConfigurationManager.AppSettings["clientSecret"],
            });
            var venueTask = foursquareService.GetVenueAsync("40a55d80f964a52020f31ee3");
            venueTask.Wait();
            var venue = venueTask.Result;
            Console.WriteLine(venue.Name);

            var venueTask2 = foursquareService.SearchVenuesAsync("Pizza", "Brno", 10);
            venueTask.Wait();
            var venues = venueTask2.Result;
            foreach (var v in venues)
            {
                Console.WriteLine(v.Name);
            }
            Console.ReadLine();
        }
    }
}
