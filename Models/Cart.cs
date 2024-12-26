using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace restapi.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        [JsonIgnore] // Bỏ qua trường này khi nhận yêu cầu POST
        public User User { get; set; }

        [Required]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [JsonIgnore] // Bỏ qua trường này khi nhận yêu cầu POST
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [JsonIgnore] // Bỏ qua trường này khi nhận yêu cầu POST
        public decimal Price { get; set; }

        [Required]
        [JsonIgnore] // Bỏ qua trường này khi nhận yêu cầu POST
        public DateTime DateAdded { get; set; }

        // Các thuộc tính từ Flutter
        [StringLength(50)]
        public string SelectedSize { get; set; } // Kích thước sản phẩm

        [StringLength(50)]
        public string SelectedSugar { get; set; } // Lượng đường

        [StringLength(50)]
        public string SelectedIce { get; set; } // Lượng đá

        [StringLength(200)]
        public string Note { get; set; } // Ghi chú
    }
}
