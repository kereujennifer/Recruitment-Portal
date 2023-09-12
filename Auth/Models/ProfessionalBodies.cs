using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Auth.Models
{
    public class ProfessionalBodies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Qualifications { get; set; }
        [DataType(DataType.Date)]
        public DateTime MemberSince { get; set; }
        public bool Ongoing { get; set; }
        public string userID { get; set; }
        public bool isEdit { get; set; }
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
