using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Models.ViewModels.Auth
{
    public class RegisterVM
    {
        public int Id  { get; set; }
        [Display(Name = "Full Name")]
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password don't match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }

        public string Role { get; set; }
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public RegisterVM Register { get; set; }
    
    }
}
