using System.Collections.Generic;
using Auth.Models;

namespace Auth.ViewModels
{
    public class SummaryViewModel
    {
        public ApplicationUser User { get; set; }
        public List<WorkExperience> WorkExperience { get; set; }
        public List<EducationalBackground> EducationalBackground { get; set; }
        public List<SeminarAndWorkshops> SeminarAndWorkshops { get; set; }
        public List<ProfessionalBodies> ProfessionalBodies { get; set; }
        public List<PracticingLicenses> PracticingLicenses { get; set; }
        public List<DocumentUploads> DocumentUploads { get; set; }







    }
}
