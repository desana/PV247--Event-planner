using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public class CreateEventViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string EventName { get; set; }

        [Display(Name = "Description")]
        public string EventDescription { get; set; }

        public string Error { get; set; }
    }
}
