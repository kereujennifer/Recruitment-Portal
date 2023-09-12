using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Auth.Models
{
    public class PracticingLicenses
    {
        public int Id { get; set; }
        public string Issuer { get; set; }
        public string Category { get; set; }
        [DataType(DataType.Date)]
        public DateTime IssuedOn { get; set; }
        [DataType(DataType.Date)]
        public DateTime ValidFrom { get; set; }
        [DataType(DataType.Date)]
        public DateTime ValidTo { get; set; }
        public string LicenseNumber { get; set; }
        public bool isEdit { get; set; }
        public string userID { get; set; }
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
