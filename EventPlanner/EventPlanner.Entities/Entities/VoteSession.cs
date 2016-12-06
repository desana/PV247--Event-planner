using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Entities.Entities
{
    internal class VoteSession
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid VoteSessionId { get; set; }

        [ForeignKey("Event")]
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; }

        public string VoterName { get; set; }

        public DateTime LastModified { get; set; }

        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
