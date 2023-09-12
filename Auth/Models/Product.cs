using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Models.Database
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Access Link")]
        public string AccessLink { get; set; }

        [Required]
        public string Description { get; set; }
        [NotMapped]
        public IFormFile? ProductImage { get; set; }
     
        public string ProductImagePath { get; set; }
        public bool isEdit { get; set; } = false;
      

    }
}
