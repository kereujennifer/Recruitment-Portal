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
    public class SeminarAndWorkshopsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeminarAndWorkshopsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SeminarAndWorkshops
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var seminarAndWorkshops = await _context.SeminarAndWorkshops
                .Where(eb => eb.userID == userId)
                .ToListAsync();

            return View(seminarAndWorkshops);
        }


        // GET: SeminarAndWorkshops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seminarAndWorkshops = await _context.SeminarAndWorkshops
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seminarAndWorkshops == null)
            {
                return NotFound();
            }

            return View(seminarAndWorkshops);
        }

        // GET: SeminarAndWorkshops/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SeminarAndWorkshops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Title,Award,From,To,userID")] SeminarAndWorkshops seminarAndWorkshops)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                seminarAndWorkshops.userID = userId;
                _context.Add(seminarAndWorkshops);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(seminarAndWorkshops);
        }

        // GET: SeminarAndWorkshops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seminarAndWorkshops = await _context.SeminarAndWorkshops.FindAsync(id);
            if (seminarAndWorkshops == null)
            {
                return NotFound();
            }
            return View(seminarAndWorkshops);
        }

        // POST: SeminarAndWorkshops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Title,Award,From,To,userID")] SeminarAndWorkshops seminarAndWorkshops)
        {
            if (id != seminarAndWorkshops.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (seminarAndWorkshops.userID != userId)
                {
                    return NotFound();
                }
                try
                {
                    _context.Update(seminarAndWorkshops);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SeminarAndWorkshopsExists(seminarAndWorkshops.Id))
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
            return View(seminarAndWorkshops);
        }

        // GET: SeminarAndWorkshops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var seminarAndWorkshops = await _context.SeminarAndWorkshops
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seminarAndWorkshops == null)
            {
                return NotFound();
            }

            return View(seminarAndWorkshops);
        }

        // POST: SeminarAndWorkshops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var seminarAndWorkshops = await _context.SeminarAndWorkshops.FindAsync(id);
            _context.SeminarAndWorkshops.Remove(seminarAndWorkshops);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SeminarAndWorkshopsExists(int id)
        {
            return _context.SeminarAndWorkshops.Any(e => e.Id == id);
        }
    }
}
