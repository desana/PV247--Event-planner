using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Entities.Entities
{
    internal class Vote
    {
        /// <summary>
        /// Primary key of the vote.
        /// </summary>
        [Key]
        public Guid VoteId { get; set; }
        
        /// <summary>
        /// Target time at place id.
        /// </summary>
        [ForeignKey("TimeAtPlace")]
        public int TimeAtPlaceId { get; set; }
        
        /// <summary>
        /// Target time at place.
        /// </summary>
        public virtual TimeAtPlace TimeAtPlace { get; set; }

        /// <summary>
        /// Number of votes for this record.
        /// </summary>
        public string Value { get; set; }
    }
}
