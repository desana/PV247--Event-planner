using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Entities.Entities
{
    internal class Vote
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid VoteId { get; set; }

        [ForeignKey("TimeAtPlace")]
        public int TimeAtPlaceId { get; set; }
    
        public virtual TimeAtPlace TimeAtPlace { get; set; }

        public string Value { get; set; }
    }
}
