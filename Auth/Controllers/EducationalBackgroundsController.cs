using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Auth.Data;
using Auth.Models;
using System.Security.Claims;
using Auth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Auth.Common.Email;

namespace Auth.Controllers
{
    public class EducationalBackgroundsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Iservice _service;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<AuthController> logger;
        private readonly IEmailSender emailSender;

        public EducationalBackgroundsController(
            UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
              //IEmailSender emailSender,
              ILogger<AuthController> logger,
            Iservice service)
        {
            this.userManager = userManager;
            _context = context;
            this.signInManager = signInManager;
            this.logger = logger;
            _service = service;
        }

        // GET: EducationalBackgrounds
      
            public async Task<IActionResult> Index()
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var educationalBackgrounds = await _context.EducationalBackground
                    .Where(eb => eb.userID == userId)
                    .ToListAsync();

                return View(educationalBackgrounds);
            }
        

        // GET: EducationalBackgrounds/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationalBackground = await _context.EducationalBackground
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationalBackground == null)
            {
                return NotFound();
            }

            return View(educationalBackground);
        }

        // GET: EducationalBackgrounds/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EducationalBackgrounds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Institution,Description,Qualification,Specialization,Grade,StartDate,EndDate,userID")] EducationalBackground educationalBackground)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                educationalBackground.userID = userId;

                _context.Add(educationalBackground);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(educationalBackground);
        }


        // GET: EducationalBackgrounds/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationalBackground = await _context.EducationalBackground.FindAsync(id);
            if (educationalBackground == null)
            {
                return NotFound();
            }
            return View(educationalBackground);
        }

        // POST: EducationalBackgrounds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Institution,Description,Qualification,Specialization,Grade,StartDate,EndDate,userID")] EducationalBackground educationalBackground)
        {
            if (id != educationalBackground.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (educationalBackground.userID != userId)
                {
                    return NotFound();
                }

                try
                {
                    _context.Update(educationalBackground);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationalBackgroundExists(educationalBackground.Id))
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
            return View(educationalBackground);
        }


        // GET: EducationalBackgrounds/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationalBackground = await _context.EducationalBackground
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationalBackground == null)
            {
                return NotFound();
            }

            return View(educationalBackground);
        }

        // POST: EducationalBackgrounds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var educationalBackground = await _context.EducationalBackground.FindAsync(id);
            _context.EducationalBackground.Remove(educationalBackground);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationalBackgroundExists(int id)
        {
            return _context.EducationalBackground.Any(e => e.Id == id);
        }
    }
}
