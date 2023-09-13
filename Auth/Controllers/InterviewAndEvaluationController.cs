using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Auth.Data;
using Auth.Models.ViewModels;
using Auth.Models;
using Microsoft.AspNetCore.Identity;
using Auth.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Auth.Common.Email;
using Auth.Common;

namespace Auth.Controllers
{
    public class InterviewAndEvaluationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Iservice _service;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IWebHostEnvironment environment;
        private readonly ILogger<AuthController> logger;
        private readonly IEmailSender emailSender;

        public InterviewAndEvaluationController(
            UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
            IWebHostEnvironment webHostEnvironment,
            IWebHostEnvironment environment,
              //IEmailSender emailSender,
              ILogger<AuthController> logger,
            Iservice service)
        {
            this.userManager = userManager;
            _context = context;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
            this.environment = environment;
            this.logger = logger;
            _service = service;
        }



        [HttpGet]
        public IActionResult AddEvaluation(int? id)
        {
            var model = new InterviewAndEvaluationViewModel();

            if (id.HasValue)
            {
                // Load data for editing
                var evaluation = _context.Evaluations.Include(e => e.Questions).FirstOrDefault(e => e.Id == id);
                if (evaluation == null)
                {
                    TempData[Constants.Error] = "Sorry, evaluation not found!";
                    return RedirectToAction("Index");
                }

                model.Id = evaluation.Id;
                model.isEdit = true;
                model.Description = evaluation.Description;
                model.Title = evaluation.Title;
                model.DateAdded = evaluation.DateAdded; // Assuming you want to keep the original date
                model.Status = evaluation.Status;

                // Convert the ICollection<QuestionData> to a List<QuestionData>
                model.Questions = evaluation.Questions.ToList();
            }

            return View("AddEvaluation", model);
        }

        [HttpPost]
        public IActionResult AddEvaluation(InterviewAndEvaluationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    var evaluation = _context.Evaluations.Include(e => e.Questions).FirstOrDefault(e => e.Id == model.Id);
                    if (evaluation == null)
                    {
                        TempData[Constants.Error] = "Sorry, evaluation not found!";
                        return RedirectToAction("Index");
                    }

                    // Update evaluation properties
                    evaluation.Description = model.Description;
                    evaluation.Title = model.Title;
                    evaluation.DateAdded = DateTime.Now;
                    evaluation.Status = "InActive";

                    foreach (var updatedQuestion in model.Questions)
                    {
                        // Find the corresponding existing question, or add as a new question if it doesn't exist
                        var existingQuestion = evaluation.Questions.FirstOrDefault(q => q.Id == updatedQuestion.Id);

                        if (existingQuestion != null)
                        {
                            // Update existing question properties
                            existingQuestion.Question = updatedQuestion.Question;
                            existingQuestion.MaxPoints = updatedQuestion.MaxPoints;
                        }
                        else
                        {
                            // If the question doesn't exist, add it
                            evaluation.Questions.Add(updatedQuestion);

                        }
                    }

                    _context.SaveChanges();
                    TempData[Constants.Success] = "Updated Successfully!";
                    return RedirectToAction("Index");
                }
                else
                {
                    // Create a new evaluation
                    Evaluation evaluation = new Evaluation
                    {
                        Description = model.Description,
                        Title = model.Title,
                        DateAdded = DateTime.Now,
                        Status = "InActive",
                        Questions = model.Questions
                    };

                    _context.Evaluations.Add(evaluation);
                    _context.SaveChanges();

                    TempData[Constants.Success] = "Added Successfully!!";
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }



        public async Task<IActionResult> DeleteEvaluation(int id)
        {
            var eval = _context.Evaluations.FirstOrDefault(c => c.Id == id);
            if (eval != null)
            {
                _context.Evaluations.Remove(eval);
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
        // GET: InterviewAndEvaluation
        public async Task<IActionResult> Index()
        {
            var model = new InterviewAndEvaluationViewModel
            {
                Evaluations = _context.Evaluations.ToList(),
            };
            return View(model);
        }


    }
}