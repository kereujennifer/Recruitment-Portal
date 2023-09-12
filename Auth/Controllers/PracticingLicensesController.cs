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

namespace Auth
{
    public class PracticingLicensesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PracticingLicensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PracticingLicenses
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var practicingLicenses = await _context.PracticingLicenses
                .Where(eb => eb.userID == userId)
                .ToListAsync();

            return View(practicingLicenses);
        }

        // GET: PracticingLicenses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practicingLicenses = await _context.PracticingLicenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (practicingLicenses == null)
            {
                return NotFound();
            }

            return View(practicingLicenses);
        }

        // GET: PracticingLicenses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PracticingLicenses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Issuer,Category,IssuedOn,ValidFrom,ValidTo,LicenseNumber,userID")] PracticingLicenses practicingLicenses)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                practicingLicenses.userID = userId;
                _context.Add(practicingLicenses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(practicingLicenses);
        }

        // GET: PracticingLicenses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practicingLicenses = await _context.PracticingLicenses.FindAsync(id);
            if (practicingLicenses == null)
            {
                return NotFound();
            }
            return View(practicingLicenses);
        }

        // POST: PracticingLicenses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Issuer,Category,IssuedOn,ValidFrom,ValidTo,LicenseNumber,userID")] PracticingLicenses practicingLicenses)
        {
            if (id != practicingLicenses.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (practicingLicenses.userID != userId)
                {
                    return NotFound();
                }
                try
                {
                    _context.Update(practicingLicenses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PracticingLicensesExists(practicingLicenses.Id))
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
            return View(practicingLicenses);
        }

        // GET: PracticingLicenses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var practicingLicenses = await _context.PracticingLicenses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (practicingLicenses == null)
            {
                return NotFound();
            }

            return View(practicingLicenses);
        }

        // POST: PracticingLicenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var practicingLicenses = await _context.PracticingLicenses.FindAsync(id);
            _context.PracticingLicenses.Remove(practicingLicenses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PracticingLicensesExists(int id)
        {
            return _context.PracticingLicenses.Any(e => e.Id == id);
        }
    }
}
