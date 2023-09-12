using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;

namespace Auth.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string AlternatePhoneNumber { get; set; }
        [DataType(DataType.Date)]

        public DateTime DateOfBirth { get; set; }
        public string IDDocument { get; set; }
        public int IDDocumentNumber { get; set; }
        public string MaritalStatus { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string Nationality { get; set; }
        public string County { get; set; }
        public string Ethnicity { get; set; }
        public string Languages { get; set; }
        public bool Disability { get; set; }
        public string DisabilityCondition { get; set; }
        public string Skills { get; set; }
        public string UserID { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool ActivationStatus { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
    }
}
