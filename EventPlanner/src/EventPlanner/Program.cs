using System.IO;
using EventPlanner.Entities;
using Microsoft.AspNetCore.Hosting;

namespace EventPlanner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var databaseContext = new EventPlannerDbContext())
            {
                Place testPlace = new Place
                {
                    Name = "testName",
                    FourSquareLink = "https://foursquare.com/v/aire-ancient-baths/4fbbd9ede4b0756c0d4c2364"
                };

                databaseContext
                    .Places
                    .Add(testPlace);

                databaseContext.SaveChanges();
            }


            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }

    }
}
