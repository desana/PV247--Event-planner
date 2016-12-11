using System;
using System.Configuration;
using System.Linq;
using FoursquareVenuesService;
using FoursquareVenuesService.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FoursquareVenuesService.Entities;

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

        [TestMethod]
        public void TestIsOpenAtTimeWithDash()
        {
            var openingHours = new Time
            {
                RenderedTime = "12:00–15:00"
            };

            Assert.IsTrue(openingHours.IsOpenAtTime(TimeSpan.Parse("12:00")));
            Assert.IsTrue(openingHours.IsOpenAtTime(TimeSpan.Parse("15:00")));
            Assert.IsTrue(openingHours.IsOpenAtTime(TimeSpan.Parse("13:10:43")));
            Assert.IsFalse(openingHours.IsOpenAtTime(TimeSpan.Parse("11:59:59")));
            Assert.IsFalse(openingHours.IsOpenAtTime(TimeSpan.Parse("15:00:01")));
            Assert.IsFalse(openingHours.IsOpenAtTime(TimeSpan.Parse("00:00:01")));
        }

        [TestMethod]
        public void TestIsOpenAtTimeWithHyphen()
        {
            var openingHours = new Time
            {
                RenderedTime = "12:00-15:00"
            };

            Assert.IsTrue(openingHours.IsOpenAtTime(TimeSpan.Parse("12:00")));
            Assert.IsTrue(openingHours.IsOpenAtTime(TimeSpan.Parse("15:00")));
            Assert.IsTrue(openingHours.IsOpenAtTime(TimeSpan.Parse("13:10:43")));
            Assert.IsFalse(openingHours.IsOpenAtTime(TimeSpan.Parse("11:59:59")));
            Assert.IsFalse(openingHours.IsOpenAtTime(TimeSpan.Parse("15:00:01")));
            Assert.IsFalse(openingHours.IsOpenAtTime(TimeSpan.Parse("00:00:01")));
        }

        [TestMethod]
        public void TestIsOpenAtTimeAfterMidnight()
        {
            var openingHours = new Time
            {
                RenderedTime = "21:00-4:00"
            };

            Assert.IsTrue(openingHours.IsOpenAtTime(TimeSpan.Parse("22:00")));
            Assert.IsTrue(openingHours.IsOpenAtTime(TimeSpan.Parse("02:00")));
            Assert.IsFalse(openingHours.IsOpenAtTime(TimeSpan.Parse("4:59:59")));
        }


        [TestMethod]
        public void TestGetOpenTimeForDayWithHyphen()
        {
            var openingHours = new Hours
            {
                Timeframes = new System.Collections.Generic.List<Timeframe>
                {
                    new Timeframe
                    {
                        Days = "Mon-Thu, Fri",
                        Open = new System.Collections.Generic.List<Time>
                        {
                            new Time
                            {
                                RenderedTime = "19:00–20:00"
                            },
                            new Time
                            {
                                RenderedTime = "12:00–15:00"
                            }
                        }
                    }
                }
            };

            Assert.IsNotNull(openingHours.GetOpenTimeForDay(DayOfWeek.Monday));
            Assert.IsNotNull(openingHours.GetOpenTimeForDay(DayOfWeek.Wednesday));
            Assert.IsNotNull(openingHours.GetOpenTimeForDay(DayOfWeek.Friday));
            Assert.IsNull(openingHours.GetOpenTimeForDay(DayOfWeek.Sunday));
        }

        [TestMethod]
        public void TestGetOpenTimeForDayWithDash()
        {
            var openingHours = new Hours
            {
                Timeframes = new System.Collections.Generic.List<Timeframe>
                {
                    new Timeframe
                    {
                        Days = "Mon–Thu, Fri",
                        Open = new System.Collections.Generic.List<Time>
                        {
                            new Time
                            {
                                RenderedTime = "19:00–20:00"
                            },
                            new Time
                            {
                                RenderedTime = "12:00–15:00"
                            }
                        }
                    }
                }
            };

            Assert.IsNotNull(openingHours.GetOpenTimeForDay(DayOfWeek.Monday));
            Assert.IsNotNull(openingHours.GetOpenTimeForDay(DayOfWeek.Wednesday));
            Assert.IsNotNull(openingHours.GetOpenTimeForDay(DayOfWeek.Friday));
            Assert.IsNull(openingHours.GetOpenTimeForDay(DayOfWeek.Sunday));
        }
    }
}
