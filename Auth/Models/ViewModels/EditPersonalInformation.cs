using System.ComponentModel.DataAnnotations;
using System;
using Auth.Models;

namespace Auth.ViewModels
{
    public class EditPersonalInformation
    {
        public EditPersonalInformation()
        {

        }
        public EditPersonalInformation(ApplicationUser user)
        {
            userID = user.Id;
            Name = user.Name;
            Title = user.Title;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
            AlternatePhoneNumber = user.AlternatePhoneNumber;
            Gender = user.Gender;
            IDDocument = user.IDDocument;
            IDDocumentNumber = user.IDDocumentNumber;
            MaritalStatus = user.MaritalStatus;
            DateOfBirth = user.DateOfBirth;
            Address = user.Address;
            Nationality = user.Nationality;
            County = user.County;
            Ethnicity = user.Ethnicity;
            Languages = user.Languages;
            Disability = user.Disability;
            DisabilityCondition = user.DisabilityCondition;
            Skills = user.Skills;
          





        }
        public string UserId { get; set; }

        public string Name { get; set; }
        public string Title { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternatePhoneNumber { get; set; }
        [DataType(DataType.Date)]

        public DateTime DateOfBirth { get; set; }
        public string IDDocument { get; set; }
        public int IDDocumentNumber { get; set; }
        public string Email { get; set; }
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
        public string? userID { get; set; }
        public bool IsLoggedIn { get; set; }
        public bool ActivationStatus { get; set; }
        public bool Active { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
    }
}
