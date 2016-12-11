using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Entities.Entities
{
    internal class VoteSession
    {
        /// <summary>
        /// Primary key of the session.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid VoteSessionId { get; set; }

        /// <summary>
        /// Id of the event to vote in.
        /// </summary>
        [ForeignKey("Event")]
        public Guid EventId { get; set; }

        /// <summary>
        /// Event to vote in.
        /// </summary>
        public virtual Event Event { get; set; }

        /// <summary>
        /// Name of the voter.
        /// </summary>
        public string VoterName { get; set; }

        /// <summary>
        /// Time when the session was last modified.
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Collection of votes for this session.
        /// </summary>
        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
