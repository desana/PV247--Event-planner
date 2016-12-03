using System;
using System.Configuration;
using System.Linq;
using FoursquareVenuesService;
using FoursquareVenuesService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EventPlanner.FoursquareVenuesService.UnitTests
{

    [TestClass]
    public class FoursquareTests
    {
        private FoursquareService _fsService;

        [TestInitialize]
        public void InitializeTests()
        {
            _fsService = new FoursquareService(new FoursquareOptions
            {
                ClientId = ConfigurationManager.AppSettings["clientId"],
                ClientSecret = ConfigurationManager.AppSettings["clientSecret"]
            });
        }

        [TestMethod]
        public void TestSearchVenuesAsync()
        {
            var task = _fsService.SearchVenuesAsync("U Karla", "Brno", 10);
            task.Wait();

            var venues = task.Result.ToList();
            Assert.IsFalse(venues.Count == 0 || venues.Count > 10);

            foreach (var venue in venues)
            {
                if (venue.Location.City != null)
                {
                    Assert.AreEqual("Brno", venue.Location.City);
                }
            }
        }

        [TestMethod]
        public void TestGetVenueAsync()
        {
            // Get id of some existing venue
            var task = _fsService.SearchVenuesAsync("U Karla", "Brno", 10);
            task.Wait();
            var result = task.Result.ToList();
            var id = result[0].Id;

            // Test GetVenueAsync
            var task2 = _fsService.GetVenueAsync(id);
            task2.Wait();
            var venue = task2.Result;

            Assert.AreEqual(id, venue.Id);
            Assert.AreEqual(result[0].Name, venue.Name);
            Assert.AreEqual(result[0].Url, venue.Url);
            Assert.AreEqual(result[0].Location.City, venue.Location.City);
        }

        [TestMethod]
        public void TestGetVenuePhotoUrlAsync()
        {
            // Get id of some existing venue
            var task = _fsService.SearchVenuesAsync("U Karla", "Brno", 10);
            task.Wait();
            var result = task.Result.ToList();
            var id = result[0].Id;

            // Test GetVenuePhotoUrlAsync
            var task2 = _fsService.GetVenuePhotoUrlAsync(id, "200x200");
            task2.Wait();
            var photo = task2.Result;

            if (!String.IsNullOrEmpty(photo))
            {
                Assert.IsTrue(photo.Contains("http"));
                Assert.IsTrue(photo.Contains("img"));
            }


        }
    }
}
