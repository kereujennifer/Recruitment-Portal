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

namespace Auth.Controllers
{
    public class ProfessionalBodiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfessionalBodiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProfessionalBodies
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var professionalBodies = await _context.ProfessionalBodies
                .Where(eb => eb.userID == userId)
                .ToListAsync();

            return View(professionalBodies);
        }

        // GET: ProfessionalBodies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professionalBodies = await _context.ProfessionalBodies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professionalBodies == null)
            {
                return NotFound();
            }

            return View(professionalBodies);
        }

        // GET: ProfessionalBodies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProfessionalBodies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Qualifications,MemberSince,Ongoing,userID")] ProfessionalBodies professionalBodies)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                professionalBodies.userID = userId;
                _context.Add(professionalBodies);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professionalBodies);
        }

        // GET: ProfessionalBodies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professionalBodies = await _context.ProfessionalBodies.FindAsync(id);
            if (professionalBodies == null)
            {
                return NotFound();
            }
            return View(professionalBodies);
        }

        // POST: ProfessionalBodies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Qualifications,MemberSince,Ongoing,userID")] ProfessionalBodies professionalBodies)
        {
            if (id != professionalBodies.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (professionalBodies.userID != userId)
                {
                    return NotFound();
                }
                try
                {
                    _context.Update(professionalBodies);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfessionalBodiesExists(professionalBodies.Id))
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
            return View(professionalBodies);
        }

        // GET: ProfessionalBodies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professionalBodies = await _context.ProfessionalBodies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professionalBodies == null)
            {
                return NotFound();
            }

            return View(professionalBodies);
        }

        // POST: ProfessionalBodies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professionalBodies = await _context.ProfessionalBodies.FindAsync(id);
            _context.ProfessionalBodies.Remove(professionalBodies);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfessionalBodiesExists(int id)
        {
            return _context.ProfessionalBodies.Any(e => e.Id == id);
        }
    }
}
