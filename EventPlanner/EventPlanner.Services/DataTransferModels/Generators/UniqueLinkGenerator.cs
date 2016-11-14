using System;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Services.DataTransferModels.Generators
{
    static class UniqueLinkGenerator
    {
        /// <summary>
        /// Generates new unique link for event.
        /// </summary>
        /// <param name="currentEvent">Event without link.</param>
        public static void GenerateUniqueLink(this EventTransferModel currentEvent)
        {
            currentEvent.Link = Guid.NewGuid().ToString();
        }
    }
}
