using System.ComponentModel.DataAnnotations;

namespace restapi.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        [Required]public string Title { get; set; } public string Subtitle { get; set; }
        public string Description { get; set; }
        [Required] public string Link { get; set; }
        public string ImageUrl { get; set; }
    }
}
