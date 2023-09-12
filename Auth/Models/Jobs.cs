using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;

namespace Auth.Models
{
    public class Jobs
    {
        public int Id { get; set; }
        public string Employer { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Industry { get; set; }
        [Display(Name ="Overall Purpose of The Job")]
        public string OverallPurpose { get; set; }
        [Display(Name = "Job Specifications/Duties")]

        public string Specifications { get; set; }
        [Display(Name = "Personal Specifications")]
        public string Status { get; set; }
        public string PersonalSpecifications { get; set; }
        [DataType(DataType.Date)]
        public DateTime ClosingDate { get; set; }
public bool IsFeatured { get; set; }

    }
}
