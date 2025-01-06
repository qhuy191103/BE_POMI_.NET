using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace restapi.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        [Range(0.01, 10000000.00)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
        public List<string> Sizes { get; set; } = new List<string>();
        public List<string> SugarLevels { get; set; } = new List<string>(); 
        public List<string> IceLevels { get; set; } = new List<string>(); public string Note { get; set; }
        [Range(1, int.MaxValue)] 
        public int Quantity { get; set; }
        public bool IsDeleted { get; set; }
    }
}
