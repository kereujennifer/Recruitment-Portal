using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Auth.Data;
using Auth.Models;
using Auth.Models.ViewModels;
using System.Security.Claims;
using Auth.Models.Database;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace Auth.Controllers
{
    public class JobApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;

        public JobApplicationsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }
        public IActionResult MyApplications()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var applications = _context.JobApplications.Where(a => a.UserId == userId).ToList();

            var viewModel = new ApplyJobViewModel
            {
                JobApplications = applications,
            };

            return View(viewModel);
        }

        // GET: JobApplications
        public async Task<IActionResult> Index()
        {
            var pendingApplicants = _context.JobApplications.Where(a => a.Status == "Pending").ToList();
            var shortlistedApplicants = _context.JobApplications.Where(a => a.Status == "Shortlisted").ToList();
            var declinedApplicants = _context.JobApplications.Where(a => a.Status == "Declined").ToList();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user's ID

            var user = await userManager.FindByIdAsync(userId);

            var model = new ApplyJobViewModel();
            model.JobApplications = _context.JobApplications.ToList();
            model.Name = user.Name;
            model.PhoneNumber = user.PhoneNumber;
            model.Email = user.Email;

            model.Title = user.Title;
            model.Gender = user.Gender;
            model.AlternatePhoneNumber = user.AlternatePhoneNumber;
            model.DateOfBirth = user.DateOfBirth;
            model.IDDocument = user.IDDocument;
            model.IDDocumentNumber = user.IDDocumentNumber;
            model.Email = user.Email;
            model.MaritalStatus = user.MaritalStatus;
            model.Address = user.Address;
            model.Nationality = user.Nationality;
            model.County = user.County;
            model.Ethnicity = user.Ethnicity;
            model.Languages = user.Languages;
            model.Disability = user.Disability;
            model.DisabilityCondition = user.DisabilityCondition;
            model.Skills = user.Skills;
            model.PendingApplicants = pendingApplicants;
            model.ShortlistedApplicants = shortlistedApplicants;
           model. DeclinedApplicants = declinedApplicants;
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
           
            if (id == null)
            {
                return NotFound();
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user's ID



            var jobApplication = await _context.JobApplications
                .FirstOrDefaultAsync(m => m.Id == id);

            if (jobApplication == null)
            {
                return NotFound();
            }
            var user = await userManager.FindByIdAsync(jobApplication.UserId);

            // Create an ApplyJobViewModel instance and populate it with data
            var viewModel = new ApplyJobViewModel
            {
                EducationalBackground = _context.EducationalBackground.Where(eb => eb.userID == jobApplication.UserId).ToList(),
                WorkExperience = _context.WorkExperience.Where(eb => eb.userID == jobApplication.UserId).ToList(),
                ProfessionalBodies = _context.ProfessionalBodies.Where(eb => eb.userID == jobApplication.UserId).ToList(),
                SeminarAndWorkshops = _context.SeminarAndWorkshops.Where(eb => eb.userID == jobApplication.UserId).ToList(),
                PracticingLicenses = _context.PracticingLicenses.Where(eb => eb.userID == jobApplication.UserId).ToList(),
                DocumentUploads = _context.DocumentUploads.Where(eb => eb.userID == jobApplication.UserId).ToList(),

                Name = jobApplication.Name,
                CoverLetter = jobApplication.CoverLetter,
                JobTitle = jobApplication.JobTitle,
                Title = user.Title,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                AlternatePhoneNumber = user.AlternatePhoneNumber,
                DateOfBirth = user.DateOfBirth,
                IDDocument = user.IDDocument,
                IDDocumentNumber = user.IDDocumentNumber,
                Email = user.Email,
                MaritalStatus = user.MaritalStatus,
                Address = user.Address,
                Nationality = user.Nationality,
                County = user.County,
                Ethnicity = user.Ethnicity,
                Languages = user.Languages,
                Disability = user.Disability,
                DisabilityCondition = user.DisabilityCondition,
                Skills = user.Skills,
                

                // Populate other properties as needed
            };

            return View(viewModel); // Pass the viewModel to the view
        }


        // GET: JobApplications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,ReferenceNumber,Status,JobTitle,DateAdded,JobId")] JobApplications jobApplications)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobApplications);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobApplications);
        }

        // GET: JobApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplications = await _context.JobApplications.FindAsync(id);
            if (jobApplications == null)
            {
                return NotFound();
            }
            return View(jobApplications);
        }

        // POST: JobApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,ReferenceNumber,Status,JobTitle,DateAdded,JobId")] JobApplications jobApplications)
        {
            if (id != jobApplications.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobApplications);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobApplicationsExists(jobApplications.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jobApplications);
        }

        // GET: JobApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplications = await _context.JobApplications
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobApplications == null)
            {
                return NotFound();
            }

            return View(jobApplications);
        }

        // POST: JobApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobApplications = await _context.JobApplications.FindAsync(id);
            _context.JobApplications.Remove(jobApplications);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobApplicationsExists(int id)
        {
            return _context.JobApplications.Any(e => e.Id == id);
        }
        [HttpPost]
        public IActionResult ChangeStatus(int id, string status)
        {
            // Load the job application from the database using the provided ID
            var jobApplication = _context.JobApplications.FirstOrDefault(ja => ja.Id == id);

            if (jobApplication == null)
            {
                return NotFound(); // Handle the case where the job application is not found
            }

            // Update the status based on the value passed from the form
            jobApplication.Status = status;

            // Save the changes to the database
            _context.SaveChanges();

            return RedirectToAction("Index"); // Redirect back to the applicant's details page
        }

    }

}
