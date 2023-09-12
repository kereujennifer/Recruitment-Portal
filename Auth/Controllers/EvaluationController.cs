using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Auth.Models;
using Auth.Data;
using System.Threading.Tasks;
using System.Linq;

namespace Auth.Controllers
{
    public class EvaluationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluationController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var evaluations = await _context.Evaluations.ToListAsync(); // Fetch all evaluations from the database
            return View(evaluations); // Pass the evaluations to the view
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Evaluation model)
        {
            if (ModelState.IsValid)
            {
                _context.Evaluations.Add(model);
                _context.SaveChanges(); // Commit changes to the database

                return RedirectToAction("Index"); // Redirect to some other page after successful submission
            }

            return View(model); // Return to the view if there are validation errors
        }
        public IActionResult Details(int id)
        {
            var evaluation = _context.Evaluations.Include(e => e.Questions).FirstOrDefault(e => e.Id == id);

            if (evaluation == null)
            {
                return NotFound();
            }

            return View(evaluation);
        }
        public IActionResult Edit(int id)
        {
            var evaluation = _context.Evaluations.Include(e => e.Questions).FirstOrDefault(e => e.Id == id);

            if (evaluation == null)
            {
                return NotFound();
            }

            return View(evaluation);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Evaluation updatedModel)
        {
            var existingEvaluation = _context.Evaluations.Include(e => e.Questions).FirstOrDefault(e => e.Id == id);

            if (existingEvaluation == null)
            {
                return NotFound();
            }

            existingEvaluation.Title = updatedModel.Title;
            existingEvaluation.Description = updatedModel.Description;

            // Update or add questions
            existingEvaluation.Questions.Clear();
            foreach (var question in updatedModel.Questions)
            {
                existingEvaluation.Questions.Add(question);
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
