using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Auth.Models.ViewModels
{
    public class ApplyJobViewModel
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; }
        public int JobId { get; set; }
            public string JobTitle { get; set; }
            public string Industry { get; set; }
            public IFormFile Resume { get; set; }
        public bool Cv { get; set; }
            public string CoverLetter { get; set; }
        public string ApplicationStatus { get; set; }
        [DataType(DataType.Date)]

        public DateTime DateAdded { get; set; }

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

        public string Institution { get; set; }
        //public string Description { get; set; }
        public string Qualification { get; set; }
        public string Specialization { get; set; }
        public string Grade { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]

        public DateTime EndDate { get; set; }
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        public bool isEdit { get; set; } = false;

        public string Employer { get; set; }
        [DataType(DataType.Date)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        public DateTime To { get; set; }
        public string WorkCategory { get; set; }
        public string JobResponsibilities { get; set; }
        //public string Name { get; set; }
        public string Qualifications { get; set; }
        [DataType(DataType.Date)]
        public DateTime MemberSince { get; set; }
        public bool Ongoing { get; set; }
        public string Type { get; set; }
        public string Award { get; set; }
        public string Issuer { get; set; }
        public string Category { get; set; }
        [DataType(DataType.Date)]
        public DateTime IssuedOn { get; set; }
        [DataType(DataType.Date)]
        public DateTime ValidFrom { get; set; }
        [DataType(DataType.Date)]
        public DateTime ValidTo { get; set; }
        public string LicenseNumber { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public ApplicationUser User { get; set; }
        public List<JobApplications> PendingApplicants { get; set; }
        public List<JobApplications> ShortlistedApplicants { get; set; }
        public List<JobApplications> DeclinedApplicants { get; set; }
        public List<WorkExperience> WorkExperience { get; set; } = new List<WorkExperience>();
        public List<EducationalBackground> EducationalBackground { get; set; } = new List<EducationalBackground>();
        public List<SeminarAndWorkshops> SeminarAndWorkshops { get; set; } = new List<SeminarAndWorkshops>();
        public List<ProfessionalBodies> ProfessionalBodies { get; set; } = new List<ProfessionalBodies>();
        public List<PracticingLicenses> PracticingLicenses { get; set; } = new List<PracticingLicenses>();
        public List<DocumentUploads> DocumentUploads { get; set; } = new List<DocumentUploads>();
        public List<JobApplications> JobApplications { get; set; } = new List<JobApplications>();
    }
}


