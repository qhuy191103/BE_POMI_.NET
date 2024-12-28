using System;
using System.ComponentModel.DataAnnotations;
namespace restapi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

     
        [StringLength(100)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        public string Phone { get; set; }

        [StringLength(200)]
        public string Address { get; set; }
        public string UserImage { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
