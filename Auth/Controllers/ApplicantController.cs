using Auth.ViewModels;
using Auth.Models.ViewModels.Auth;
using Auth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using System;
using Microsoft.Extensions.Logging;
using Auth.Data;
using Auth.Models;
using Auth.Common;
using Auth.Common.Email;
using System.Security.Claims;
using Auth.Models.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Auth.Models.Database;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using Microsoft.Extensions.Hosting;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace Auth.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Iservice _service;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IWebHostEnvironment environment;
        private readonly ILogger<AuthController> logger;
        private readonly IEmailSender emailSender;

        public ApplicantController(
            UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment webHostEnvironment,
            IWebHostEnvironment environment,
              //IEmailSender emailSender,
              ILogger<AuthController> logger,
            Iservice service)
        {
            this.userManager = userManager;
            this._context = context;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
            this.environment = environment;
            this.logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user's ID
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                // Handle the case when user is not found
                return RedirectToAction("Index"); // Redirect to a suitable page
            }

            var model = new JobProfileViewModel
            {
                EducationalBackground = _context.EducationalBackground.Where(eb => eb.userID == userId).ToList(),
                WorkExperience = _context.WorkExperience.Where(eb => eb.userID == userId).ToList(),
                ProfessionalBodies = _context.ProfessionalBodies.Where(eb => eb.userID == userId).ToList(),
                SeminarAndWorkshops = _context.SeminarAndWorkshops.Where(eb => eb.userID == userId).ToList(),
                PracticingLicenses = _context.PracticingLicenses.Where(eb => eb.userID == userId).ToList(),
                DocumentUploads = _context.DocumentUploads.Where(eb => eb.userID == userId).ToList(),



                // Populate other properties from the user
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Title = user.Title,
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
                Skills = user.Skills
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult ViewPersonalInformation()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user's ID
            var user = userManager.FindByIdAsync(userId).Result;

            if (user == null)
            {
                // Handle the case when user is not found
                return RedirectToAction("Index", "Applicant"); // Redirect to a suitable page
            }

            var personalInfoViewModel = new JobProfileViewModel(user);

            return View(personalInfoViewModel);
        }


        [HttpGet]
        public IActionResult EditPersonalInformation()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user's ID
            var user = userManager.FindByIdAsync(userId).Result;

            if (user == null)
            {
                // Handle the case when user is not found
                return RedirectToAction("Index", "Applicant"); // Redirect to a suitable page
            }

            var personalInfoViewModel = new JobProfileViewModel(user);

            return View(personalInfoViewModel);
        }

        [HttpPost]
        public IActionResult EditPersonalInformation(JobProfileViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user's ID
            var user = userManager.FindByIdAsync(userId).Result;

            if (user == null)
            {
                // Handle the case when user is not found
                return RedirectToAction("Index", "Applicant"); // Redirect to a suitable page
            }
            // Update the user's properties based on viewModel properties
            user.Name = viewModel.Name;
            user.PhoneNumber = viewModel.PhoneNumber;
            user.Title = viewModel.Title;
            user.Gender = viewModel.Gender;
            user.AlternatePhoneNumber = viewModel.AlternatePhoneNumber;
            user.DateOfBirth = viewModel.DateOfBirth;
            user.IDDocument = viewModel.IDDocument;
            user.IDDocumentNumber = viewModel.IDDocumentNumber;
            user.Email = viewModel.Email;
            user.MaritalStatus = viewModel.MaritalStatus;
            user.Address = viewModel.Address;
            user.Nationality = viewModel.Nationality;
            user.County = viewModel.County;
            user.Ethnicity = viewModel.Ethnicity;
            user.Languages = viewModel.Languages;
            user.Disability = viewModel.Disability;
            user.DisabilityCondition = viewModel.DisabilityCondition;
            user.Skills = viewModel.Skills;

            var updateResult = userManager.UpdateAsync(user).Result;

            if (updateResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Handle update failure
                // You can display error messages in the view or redirect to an error page
                return View(viewModel); // Return the view with validation errors or error messages
            }
        }
        public async Task<IActionResult> EducationalBackGround()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var educationalBackgrounds = await _context.EducationalBackground
                .Where(eb => eb.userID == userId)
                .ToListAsync();

            return View(educationalBackgrounds);
        }


        public async Task<IActionResult> Summary()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);


            var model = new SummaryViewModel
            {
                EducationalBackground = _context.EducationalBackground.Where(eb => eb.userID == userId).ToList(),
                WorkExperience = _context.WorkExperience.Where(eb => eb.userID == userId).ToList(),
                ProfessionalBodies = _context.ProfessionalBodies.Where(eb => eb.userID == userId).ToList(),
                SeminarAndWorkshops = _context.SeminarAndWorkshops.Where(eb => eb.userID == userId).ToList(),
                PracticingLicenses = _context.PracticingLicenses.Where(eb => eb.userID == userId).ToList(),
                DocumentUploads = _context.DocumentUploads.Where(eb => eb.userID == userId).ToList(),

                // Set other properties of the view model

            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddEducationalBackground(int Id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var education = new JobProfileViewModel();
            if (Id > 0)
            {

                var educationalBackground = _context.EducationalBackground.FirstOrDefault(d => d.Id == Id);
                if (educationalBackground == null)
                    if (educationalBackground == null)
                    {
                        TempData[Constants.Error] = "Sorry, educational background not found!";
                        return RedirectToAction("Index");
                    }
                education.userID = user.Id;

                education.isEdit = true;
                education.Institution = educationalBackground.Institution;
                education.Qualification = educationalBackground.Qualification;
                education.Specialization = educationalBackground.Specialization;
                education.Grade = educationalBackground.Grade;
                education.StartDate = educationalBackground.StartDate;
                education.EndDate = educationalBackground.EndDate;
                _context.SaveChanges();
            }
            return View(education);
        }
        [HttpPost]
        public async Task<IActionResult> AddEducationalBackground(JobProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (model.Id > 0)
                {
                    var education = _context.EducationalBackground.FirstOrDefault(d => d.Id == model.Id);
                    if (education == null)
                        if (education == null)
                        {
                            TempData[Constants.Error] = "Sorry, Educational Background not found!";
                            return RedirectToAction("Index");
                        }
                    education.userID = user.Id;
                    education.isEdit = true;
                    education.Institution = model.Institution;
                    education.Qualification = model.Qualification;
                    education.Specialization = model.Specialization;
                    education.Grade = model.Grade;
                    education.StartDate = model.StartDate;
                    education.EndDate = model.EndDate;
                    _context.EducationalBackground.Update(education);
                    _context.SaveChanges();
                    TempData[Constants.Success] = "Updated Successful!";
                    return RedirectToAction("Index");
                }
                else
                {
                    EducationalBackground newEducationalBackground = new EducationalBackground
                    {
                        userID = user.Id,
                        Institution = model.Institution,
                        Qualification = model.Qualification,
                        Specialization = model.Specialization,
                        Grade = model.Grade,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate

                    };
                    _context.EducationalBackground.Add(newEducationalBackground);
                    _context.SaveChanges();

                    TempData[Constants.Success] = "Added Successfully!!";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteEducationalBackGround(int id)
        {
            var edurm = _context.EducationalBackground.FirstOrDefault(c => c.Id == id);
            if (edurm != null)
            {
                _context.EducationalBackground.Remove(edurm);
                _context.SaveChanges();
                TempData[Constants.Success] = "Deleted Successfully!!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData[Constants.Error] = "Sorry, record not found!";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> AddWorkExperience(int Id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var work = new JobProfileViewModel();
            if (Id > 0)
            {
                var workExperience = _context.WorkExperience.FirstOrDefault(d => d.Id == Id);
                if (workExperience == null)
                    if (workExperience == null)
                    {
                        TempData[Constants.Error] = "Sorry, work experience not found!";
                        return RedirectToAction("Index");
                    }
                work.userID = user.Id;

                work.isEdit = true;
                work.Employer = workExperience.Employer;
                work.JobTitle = workExperience.JobTitle;
                work.From = workExperience.From;
                work.To = workExperience.To;
                work.Industry = workExperience.Industry;
                work.WorkCategory = workExperience.WorkCategory;
                work.JobResponsibilities = workExperience.JobResponsibilities;
                _context.SaveChanges();
            }
            return View(work);
        }
        [HttpPost]
        public async Task<IActionResult> AddWorkExperience(JobProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (model.Id > 0)
                {
                    var workExperience = _context.WorkExperience.FirstOrDefault(d => d.Id == model.Id);
                    if (workExperience == null)
                        if (workExperience == null)
                        {
                            TempData[Constants.Error] = "Sorry, Work Experience not found!";
                            return RedirectToAction("Index");
                        }
                    workExperience.userID = user.Id;

                    workExperience.isEdit = true;
                    workExperience.Employer = model.Employer;
                    workExperience.JobTitle = model.JobTitle;
                    workExperience.From = model.From;
                    workExperience.To = model.To;
                    workExperience.Industry = model.Industry;
                    workExperience.WorkCategory = model.WorkCategory;
                    workExperience.JobResponsibilities = model.JobResponsibilities;
                    _context.WorkExperience.Update(workExperience);
                    _context.SaveChanges();
                    TempData[Constants.Success] = "Updated Successful!";
                    return RedirectToAction("Index");
                }
                else
                {
                    WorkExperience newWorkExperience = new WorkExperience
                    {
                        userID = user.Id,

                        Employer = model.Employer,
                        JobTitle = model.JobTitle,
                        From = model.From,
                        To = model.To,
                        Industry = model.Industry,
                        JobResponsibilities = model.JobResponsibilities

                    };
                    _context.WorkExperience.Add(newWorkExperience);
                    _context.SaveChanges();

                    TempData[Constants.Success] = "Added Successfully!!";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteWorkExperience(int id)
        {
            var edurm = _context.WorkExperience.FirstOrDefault(c => c.Id == id);
            if (edurm != null)
            {
                _context.WorkExperience.Remove(edurm);
                _context.SaveChanges();
                TempData[Constants.Success] = "Deleted Successfully!!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData[Constants.Error] = "Sorry, record not found!";
                return RedirectToAction("Index");
            }

        }
        [HttpGet]
        public async Task<IActionResult> AddProfessionalBodies(int Id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var professional = new JobProfileViewModel();
            if (Id > 0)
            {
                var proffesionalBodies = _context.ProfessionalBodies.FirstOrDefault(d => d.Id == Id);
                if (proffesionalBodies == null)
                    if (proffesionalBodies == null)
                    {
                        TempData[Constants.Error] = "Sorry, educational background not found!";
                        return RedirectToAction("Index");
                    }
                professional.userID = user.Id;

                professional.isEdit = true;
                professional.Name = proffesionalBodies.Name;
                professional.MemberSince = proffesionalBodies.MemberSince;
                professional.Ongoing = proffesionalBodies.Ongoing;
                professional.Qualifications = proffesionalBodies.Qualifications;

                _context.SaveChanges();
            }
            return View(professional);
        }
        [HttpPost]
        public async Task<IActionResult> AddProfessionalBodies(JobProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (model.Id > 0)
                {
                    var proffesionalBodies = _context.ProfessionalBodies.FirstOrDefault(d => d.Id == model.Id);
                    if (proffesionalBodies == null)
                        if (proffesionalBodies == null)
                        {
                            TempData[Constants.Error] = "Sorry, Educational Background not found!";
                            return RedirectToAction("Index");
                        }
                    proffesionalBodies.userID = user.Id;

                    proffesionalBodies.isEdit = true;
                    proffesionalBodies.MemberSince = model.MemberSince;
                    proffesionalBodies.Ongoing = model.Ongoing;
                    proffesionalBodies.Qualifications = model.Qualifications;
                    proffesionalBodies.Name = model.Name;

                    _context.ProfessionalBodies.Update(proffesionalBodies);
                    _context.SaveChanges();
                    TempData[Constants.Success] = "Updated Successful!";
                    return RedirectToAction("Index");
                }
                else
                {
                    ProfessionalBodies newprofessionalBodies = new ProfessionalBodies
                    {
                        userID = user.Id,

                        MemberSince = model.MemberSince,
                        Ongoing = model.Ongoing,
                        Qualifications = model.Qualifications,
                        Name = model.Name

                    };
                    _context.ProfessionalBodies.Add(newprofessionalBodies);
                    _context.SaveChanges();

                    TempData[Constants.Success] = "Added Successfully!!";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteProffesionalBodies(int id)
        {
            var edurm = _context.ProfessionalBodies.FirstOrDefault(c => c.Id == id);
            if (edurm != null)
            {
                _context.ProfessionalBodies.Remove(edurm);
                _context.SaveChanges();
                TempData[Constants.Success] = "Deleted Successfully!!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData[Constants.Error] = "Sorry, record not found!";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AddSeminarsAndWorkShops(int Id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var seminars = new JobProfileViewModel();
            if (Id > 0)
            {
                var seminarsAndWorkShops = _context.SeminarAndWorkshops.FirstOrDefault(d => d.Id == Id);
                if (seminarsAndWorkShops == null)
                    if (seminarsAndWorkShops == null)
                    {
                        TempData[Constants.Error] = "Sorry, educational background not found!";
                        return RedirectToAction("Index");
                    }
                seminars.userID = user.Id;

                seminars.isEdit = true;
                seminars.Type = seminarsAndWorkShops.Type;
                seminars.Title = seminarsAndWorkShops.Title;
                seminars.Award = seminarsAndWorkShops.Award;
                seminars.From = seminarsAndWorkShops.From;
                seminars.To = seminarsAndWorkShops.To;


                _context.SaveChanges();
            }
            return View(seminars);
        }
        [HttpPost]
        public async Task<IActionResult> AddSeminarsAndWorkShops(JobProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (model.Id > 0)
                {
                    var seminarsAndWorkShops = _context.SeminarAndWorkshops.FirstOrDefault(d => d.Id == model.Id);
                    if (seminarsAndWorkShops == null)
                        if (seminarsAndWorkShops == null)
                        {
                            TempData[Constants.Error] = "Sorry, Educational Background not found!";
                            return RedirectToAction("Index");
                        }
                    seminarsAndWorkShops.userID = user.Id;

                    seminarsAndWorkShops.isEdit = true;
                    seminarsAndWorkShops.Type = model.Type;
                    seminarsAndWorkShops.Title = model.Title;
                    seminarsAndWorkShops.Award = model.Award;
                    seminarsAndWorkShops.From = model.From;
                    seminarsAndWorkShops.To = model.To;


                    _context.SeminarAndWorkshops.Update(seminarsAndWorkShops);
                    _context.SaveChanges();
                    TempData[Constants.Success] = "Updated Successful!";
                    return RedirectToAction("Index");
                }
                else
                {
                    SeminarAndWorkshops seminarAndWorkshops = new SeminarAndWorkshops
                    {
                        userID = user.Id,
                        Title = model.Title,
                        Type = model.Type,
                        Award = model.Award,
                        From = model.From,
                        To = model.To,
                    };
                    _context.SeminarAndWorkshops.Add(seminarAndWorkshops);
                    _context.SaveChanges();

                    TempData[Constants.Success] = "Added Successfully!!";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeleteSeminarsAndWorkshops(int id)
        {
            var edurm = _context.SeminarAndWorkshops.FirstOrDefault(c => c.Id == id);
            if (edurm != null)
            {
                _context.SeminarAndWorkshops.Remove(edurm);
                _context.SaveChanges();
                TempData[Constants.Success] = "Deleted Successfully!!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData[Constants.Error] = "Sorry, record not found!";
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> AddPracticingLicenses(int Id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var licenses = new JobProfileViewModel();
            if (Id > 0)
            {
                var practicingLicenses = _context.PracticingLicenses.FirstOrDefault(d => d.Id == Id);
                if (practicingLicenses == null)
                    if (practicingLicenses == null)
                    {
                        TempData[Constants.Error] = "Sorry, work expirience not found!";
                        return RedirectToAction("Index");
                    }
                licenses.userID = user.Id;

                licenses.isEdit = true;
                licenses.LicenseNumber = practicingLicenses.LicenseNumber;
                licenses.Issuer = practicingLicenses.Issuer;
                licenses.Category = practicingLicenses.Category;
                licenses.IssuedOn = practicingLicenses.IssuedOn;
                licenses.ValidFrom = practicingLicenses.ValidFrom;
                licenses.ValidTo = practicingLicenses.ValidTo;
                _context.SaveChanges();
            }
            return View(licenses);
        }
        [HttpPost]
        public async Task<IActionResult> AddPracticingLicenses(JobProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (model.Id > 0)
                {
                    var practicingLicenses = _context.PracticingLicenses.FirstOrDefault(d => d.Id == model.Id);
                    if (practicingLicenses == null)
                        if (practicingLicenses == null)
                        {
                            TempData[Constants.Error] = "Sorry, Work Experience not found!";
                            return RedirectToAction("Index");
                        }
                    practicingLicenses.userID = user.Id;

                    practicingLicenses.isEdit = true;
                    practicingLicenses.Issuer = model.Issuer;
                    practicingLicenses.Category = model.Category;
                    practicingLicenses.IssuedOn = model.IssuedOn;
                    practicingLicenses.ValidFrom = model.ValidFrom;
                    practicingLicenses.ValidTo = model.ValidTo;
                    practicingLicenses.LicenseNumber = model.LicenseNumber;
                    _context.PracticingLicenses.Update(practicingLicenses);
                    _context.SaveChanges();
                    TempData[Constants.Success] = "Updated Successful!";
                    return RedirectToAction("Index");
                }
                else
                {
                    PracticingLicenses practicingLicenses = new PracticingLicenses
                    {
                        userID = user.Id,
                        Issuer = model.Issuer,
                        Category = model.Category,
                        IssuedOn = model.IssuedOn,
                        ValidFrom = model.ValidFrom,
                        ValidTo = model.ValidTo,
                        LicenseNumber = model.LicenseNumber

                    };
                    _context.PracticingLicenses.Add(practicingLicenses);
                    _context.SaveChanges();

                    TempData[Constants.Success] = "Added Successfully!!";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
        public async Task<IActionResult> DeletePracticingLicenses(int id)
        {
            var edurm = _context.PracticingLicenses.FirstOrDefault(c => c.Id == id);
            if (edurm != null)
            {
                _context.PracticingLicenses.Remove(edurm);
                _context.SaveChanges();
                TempData[Constants.Success] = "Deleted Successfully!!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData[Constants.Error] = "Sorry, record not found!";
                return RedirectToAction("Index");
            }

        }
        [HttpGet]
        public IActionResult AddDocumentUpload(int id)
        {
            var documentUpload = new JobProfileViewModel();

            if (id > 0)
            {
                var uploadFiles = _context.DocumentUploads.FirstOrDefault(d => d.Id == id);

                if (uploadFiles == null)
                {
                    TempData[Constants.Error] = "Sorry, document Uploads not found!";
                    return RedirectToAction("Index");
                }

                // Populate the view model with data from the database

                documentUpload.Id = uploadFiles.Id;
                documentUpload.Title = uploadFiles.Title;
                documentUpload.Description = uploadFiles.Description;
                documentUpload.FilePath = uploadFiles.FilePath;
                documentUpload.FileSize = uploadFiles.FileSize;
            }

            return View(documentUpload); // Pass the view model to the view
        }


        [HttpPost]
        public async Task<IActionResult> AddDocumentUpload(JobProfileViewModel model, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);

                if (model.File != null)
                {
                    // Save the file to the "uploads" folder and update model.FilePath
                    string uploadDir = Path.Combine(environment.WebRootPath, "uploads");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                    string filePath = Path.Combine(uploadDir, uniqueFileName);

                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.File.CopyToAsync(fileStream);
                    }

                    model.FilePath = uniqueFileName; // Update the FilePath property
                }

                if (model.Id > 0)
                {
                    // Update the existing record
                    var uploadFiles = _context.DocumentUploads.FirstOrDefault(d => d.Id == model.Id);

                    if (uploadFiles == null)
                    {
                        TempData[Constants.Error] = "Sorry, document Uploads not found!";
                        return RedirectToAction("Index");
                    }
                    uploadFiles.userID = user.Id;

                    uploadFiles.Title = model.Title;
                    uploadFiles.Description = model.Description;
                    uploadFiles.FilePath = model.FilePath;
                    uploadFiles.FileSize = model.FileSize;
                }
                else
                {
                    long fileSizeInBytes = file.Length;
                    string fileSizeText = FormatFileSize(fileSizeInBytes);

                    // Create a new DocumentUploads object and populate its properties
                    DocumentUploads documentUploads = new DocumentUploads
                    {
                        userID = user.Id,
                        Title = model.Title,
                        Description = model.Description,
                        FilePath = model.FilePath,

                        FileSize = fileSizeText, // Store the formatted file size
                        // Set other properties as needed
                    };

                    _context.DocumentUploads.Add(documentUploads);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index"); // Redirect after successful update
                }
            }

            // If the model is not valid or the file is missing/empty, return to the same view with errors
            return View(model);
        }

        private string FormatFileSize(long bytes)
        {
            // Convert bytes to a human-readable format (e.g., KB, MB, GB, etc.)
            string[] sizes = { "B", "KB", "MB", "GB" };
            int order = 0;
            while (bytes >= 1024 && order < sizes.Length - 1)
            {
                bytes /= 1024;
                order++;
            }
            return $"{bytes:0.##} {sizes[order]}";
        }


        public async Task<IActionResult> DeleteDocumentUpload(int id)
        {
            var documentUpload = _context.DocumentUploads.FirstOrDefault(c => c.Id == id);
            if (documentUpload != null)
            {
                // Delete the associated file from the "uploads" folder
                string filePath = Path.Combine(environment.WebRootPath, "uploads", documentUpload.FilePath);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _context.DocumentUploads.Remove(documentUpload);
                await _context.SaveChangesAsync();
                TempData[Constants.Success] = "Deleted Successfully!!";
            }
            else
            {
                TempData[Constants.Error] = "Sorry, record not found!";
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ApplyJob(int id)
        {
            var job = _context.Jobs.FirstOrDefault(j => j.Id == id);
            if (job == null)
            {
                return NotFound(); // Handle the case where the job is not found
            }

            var applyViewModel = new ApplyJobViewModel
            {
                JobId = job.Id,
                JobTitle = job.Title,
                Industry = job.Industry,
                Employer=job.Employer,
                //Add Location
                // Add other job-related properties as needed
            };

            return View(applyViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ApplyJob(ApplyJobViewModel model)
        {
            string GetNextNumericPart()
            {
                var employerYear = $"{model.Employer}{DateTime.Now.Year}";

                var lastReferenceNumber = _context.JobApplications
                    .Where(a => a.ReferenceNumber.StartsWith(employerYear))
                    .OrderByDescending(a => a.ReferenceNumber)
                    .Select(a => a.ReferenceNumber)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(lastReferenceNumber))
                {
                    return "0001";
                }
                else
                {
                    var numericPart = lastReferenceNumber.Substring(employerYear.Length + 1);
                    if (int.TryParse(numericPart, out var numericValue))
                    {
                        numericValue++;
                        return numericValue.ToString("D4");
                    }
                    else
                    {
                        return "0001";
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var yearPart = DateTime.Now.Year.ToString();
                var numericPart = GetNextNumericPart();
                var referenceNumber = $"{yearPart}-{numericPart:D4}";
                var user = await userManager.FindByIdAsync(userId);

                // Check if the user has already applied for the job
                var existingApplication = _context.JobApplications
                    .FirstOrDefault(ja => ja.UserId == userId && ja.JobId == model.JobId);

                if (existingApplication != null)
                {
                    // User has already applied for this job, show a message or handle accordingly.
                    // You can redirect to a page displaying a message or take other actions.
                    return RedirectToAction("AlreadyApplied", "JobApplications");
                }

                // Create a new JobApplication entity
                var jobApplication = new JobApplications
                {
                    UserId = userId,
                    Name = user.Name,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    JobId = model.JobId,
                    JobTitle = model.JobTitle,
                    DateAdded = DateTime.Now,
                    ReferenceNumber = referenceNumber,
                    Status = "Pending",
                    CoverLetter = model.CoverLetter,  // Save the cover letter
                    Cv = model.Cv
                    // Set other application-related properties
                };

                // Save the job application to the database
                _context.JobApplications.Add(jobApplication);
                _context.SaveChanges();

                // Update the job's status from Featured to Applied
                var job = _context.Jobs.Find(model.JobId);
                if (job != null)
                {
                    job.Status = "Applied";
                    _context.SaveChanges();
                }

                return RedirectToAction("MyApplications", "JobApplications");
            }


            // Handle validation errors and return the view with error messages
            return View(model);
        }
      
          
     

       


    }
}

