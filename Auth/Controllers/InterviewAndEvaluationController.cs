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
        public async Task<IActionResult> AddInterView(int Id)
        {

            var eval = new InterviewAndEvaluationViewModel();
            if (Id > 0)
            {

                var evaluation = _context.Evaluations.FirstOrDefault(d => d.Id == Id);
                if (evaluation == null)
                    if (evaluation == null)
                    {
                        TempData[Constants.Error] = "Sorry, evaluation not found!";
                        return RedirectToAction("Index");
                    }

                eval.isEdit = true;
                eval.Description = evaluation.Description;
                eval.Title = evaluation.Title;
              
                eval.Questions = evaluation.Questions;
                _context.SaveChanges();
            }
            return View(eval);
        }
        [HttpPost]
        public async Task<IActionResult> AddInterView(InterviewAndEvaluationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    var evaluation = _context.Evaluations.FirstOrDefault(d => d.Id == model.Id);
                    if (evaluation == null)
                        if (evaluation == null)
                        {
                            TempData[Constants.Error] = "Sorry, evaluation not found!";
                            return RedirectToAction("Index");
                        }
                    evaluation.isEdit = true;
                    evaluation.Description = model.Description;
                    evaluation.Title = model.Title;
                    evaluation.DateAdded = DateTime.Now;
                    evaluation.Status = "InActive";
                    evaluation.Questions = model.Questions;

                    _context.Evaluations.Update(evaluation);
                    _context.SaveChanges();
                    TempData[Constants.Success] = "Updated Successful!";
                    return RedirectToAction("Index");
                }
                else
                {
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
        public async Task<IActionResult> DeleteInterView(int id)
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
        public async Task<IActionResult> AddEvaluation(InterviewAndEvaluationViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    var evaluation = _context.Evaluations.Include(e => e.Questions).FirstOrDefault(e => e.Id == id);
                    if (evaluation == null)
                    {
                        TempData[Constants.Error] = "Sorry, evaluation not found!";
                        return RedirectToAction("Index");
                    }

                    evaluation.isEdit = true;
                    evaluation.Description = model.Description;
                    evaluation.Title = model.Title;
                    evaluation.DateAdded = DateTime.Now;
                    evaluation.Status = "InActive";

                    // Clear the existing questions
                    evaluation.Questions.Clear();

                    // Add the updated questions from the model
                    foreach (var question in model.Questions)
                    {
                        evaluation.Questions.Add(question);
                    }

                    _context.Evaluations.Update(evaluation);
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

        // GET: InterviewAndEvaluation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interviewAndEvaluationViewModel = await _context.InterviewAndEvaluationViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interviewAndEvaluationViewModel == null)
            {
                return NotFound();
            }

            return View(interviewAndEvaluationViewModel);
        }

        // GET: InterviewAndEvaluation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InterviewAndEvaluation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,MaxPoints,Venue,InterViewDate,Question")] InterviewAndEvaluationViewModel interviewAndEvaluationViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(interviewAndEvaluationViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(interviewAndEvaluationViewModel);
        }

        // GET: InterviewAndEvaluation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interviewAndEvaluationViewModel = await _context.InterviewAndEvaluationViewModel.FindAsync(id);
            if (interviewAndEvaluationViewModel == null)
            {
                return NotFound();
            }
            return View(interviewAndEvaluationViewModel);
        }

        // POST: InterviewAndEvaluation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,MaxPoints,Venue,InterViewDate,Question")] InterviewAndEvaluationViewModel interviewAndEvaluationViewModel)
        {
            if (id != interviewAndEvaluationViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(interviewAndEvaluationViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InterviewAndEvaluationViewModelExists(interviewAndEvaluationViewModel.Id))
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
            return View(interviewAndEvaluationViewModel);
        }

        // GET: InterviewAndEvaluation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var interviewAndEvaluationViewModel = await _context.InterviewAndEvaluationViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (interviewAndEvaluationViewModel == null)
            {
                return NotFound();
            }

            return View(interviewAndEvaluationViewModel);
        }

        // POST: InterviewAndEvaluation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var interviewAndEvaluationViewModel = await _context.InterviewAndEvaluationViewModel.FindAsync(id);
            _context.InterviewAndEvaluationViewModel.Remove(interviewAndEvaluationViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InterviewAndEvaluationViewModelExists(int id)
        {
            return _context.InterviewAndEvaluationViewModel.Any(e => e.Id == id);
        }
    }
}
