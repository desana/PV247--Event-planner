using System;

namespace EventPlanner.Services.DataTransferModels.Generators
{
    static class UniqueLinkGenerator
    {
        /// <summary>
        /// Generates new unique link for event.
        /// </summary>
        /// <param name="currentEvent">Event without link.</param>
        public static void GenerateUniqueLink(this Event currentEvent)
        {
            currentEvent.EventLink = Guid.NewGuid().ToString();
        }
    }
}
