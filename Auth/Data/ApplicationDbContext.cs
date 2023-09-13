using Auth.Models.Database;
using Auth.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Auth.Models.ViewModels;

namespace Auth.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Product { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Auth.Models.WorkExperience> WorkExperience { get; set; }
        public DbSet<Auth.Models.EducationalBackground> EducationalBackground { get; set; }
        public DbSet<Auth.Models.PracticingLicenses> PracticingLicenses { get; set; }
        public DbSet<Auth.Models.ProfessionalBodies> ProfessionalBodies { get; set; }
        public DbSet<Auth.Models.SeminarAndWorkshops> SeminarAndWorkshops { get; set; }
        public DbSet<Auth.Models.DocumentUploads> DocumentUploads { get; set; }
        public DbSet<Auth.Models.Jobs> Jobs { get; set; }
        public DbSet<Auth.Models.JobApplications> JobApplications { get; set; }
        public DbSet<Auth.Models.Interview> Interview { get; set; }

        public DbSet<Evaluation> Evaluations { get; set; }
        public DbSet<QuestionData> Questions { get; set; }
        //public DbSet<Auth.Models.ViewModels.InterviewAndEvaluationViewModel> InterviewAndEvaluationViewModel { get; set; }

    }
}
