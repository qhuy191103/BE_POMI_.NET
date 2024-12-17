using System.ComponentModel.DataAnnotations;

namespace restapi.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Subtitle { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required, StringLength(200)]
        public string Link { get; set; }
    }
}
