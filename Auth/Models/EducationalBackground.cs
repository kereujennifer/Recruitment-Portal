using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Auth.Models
{
    public class EducationalBackground
    {
        public int Id { get; set; }
        public string Institution { get; set; }
        //public string Description { get; set; }
        public string Qualification { get; set; }
        public string Specialization { get; set; }
        public string Grade { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]

        public DateTime EndDate { get; set; }
        public string userID { get; set; }
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public bool isEdit { get; set; } = false;



    }
}
