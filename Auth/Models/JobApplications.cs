using System.Collections.Generic;
using System;

namespace Auth.Models
{
    public class JobApplications
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ReferenceNumber { get; set; }
        public string Status { get; set; }
        public string JobTitle { get; set; }
        public DateTime DateAdded { get; set; }
        public int JobId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string CoverLetter { get; set; }  // Add this property for the cover letter
        public bool Cv { get; set; }
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
            public ApplicationUser User { get; set; }

    }
}
