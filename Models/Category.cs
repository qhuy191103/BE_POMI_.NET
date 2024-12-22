﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace restapi.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        public string CategoryImage { get; set; }
        [JsonIgnore]
        public List<Product>? Products { get; set; }
    }
}
