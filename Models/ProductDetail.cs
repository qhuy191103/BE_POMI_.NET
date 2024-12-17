using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace restapi.Models
{
    public class ProductDetail
    {
        [Key, ForeignKey("Product")] public int ProductId { get; set; }
        public Product Product { get; set; }
        public List<string> Sizes { get; set; } = new List<string>(); 
        public List<string> SugarLevels { get; set; } = new List<string>(); 
        public List<string> IceLevels { get; set; } = new List<string>();
        public string Note { get; set; }
        [Range(1, int.MaxValue)] 
        public int Quantity { get; set; }
    }
}
