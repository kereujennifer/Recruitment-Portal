using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class WorkExperience
    {
        public int Id { get; set; }
        public string Employer { get; set; }
        public string JobTitle { get; set; }
        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
        public string Industry { get; set; }
        public string WorkCategory { get; set; }
        public string JobResponsibilities { get; set; }
        public string userID { get; set; }
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public bool isEdit { get; set; }
    }
}
