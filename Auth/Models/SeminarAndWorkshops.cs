using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Auth.Models
{
    public class SeminarAndWorkshops
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Award { get; set; }
        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
        public string userID { get; set; }
        public string Institution { get; set; }
        public bool isEdit { get; set; }
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
    }
}
