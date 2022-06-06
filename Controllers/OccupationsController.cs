using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NbdAplication.Data;
using NbdAplication.Models;

namespace NbdAplication.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class OccupationsController : Controller
    {
        private readonly NbdContext _context;

        public OccupationsController(NbdContext context)
        {
            _context = context;
        }

        // GET: Occupations
        public async Task<IActionResult> Index(string sortOccupation, string searchString)
        {
            ViewData["occupationPosition"] = String.IsNullOrEmpty(sortOccupation) ? "occupation_desc" : "occupation";
            ViewData["occupationPrice"] = sortOccupation == "occupationPrice" ? "occupationPrice_desc" : "occupationPrice";
            ViewData["SearchFilter"] = searchString;

            var occupations = from o in _context.Occupations
                            select o;
            if (!String.IsNullOrEmpty(searchString))
            {
                occupations = occupations.Where(o => o.Position.Contains(searchString)
                                             || o.Description.Contains(searchString));
            }
            switch (sortOccupation)
            {
                case "occupation":
                    occupations = occupations.OrderBy(o => o.Position);
                    break;
                case "occupation_desc":
                    occupations = occupations.OrderByDescending(o => o.Position);
                    break;
                case "occupationPrice":
                    occupations = occupations.OrderBy(o => o.PricePerHour);
                    break;
                case "occupationPrice_desc":
                    occupations = occupations.OrderByDescending(o => o.PricePerHour);
                    break;
                default:
                    occupations = occupations.OrderBy(o => o.Position);
                    break;
            }
            
            ViewBag.isAdmin = false;

            if (User.IsInRole("Admin"))
            {
                ViewBag.isAdmin = true;
            }



            return View(await occupations.AsNoTracking().ToListAsync());
        }

        // GET: Occupations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = await _context.Occupations
                .FirstOrDefaultAsync(m => m.ID == id);
            if (occupation == null)
            {
                return NotFound();
            }

            return View(occupation);
        }

        // GET: Occupations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Occupations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Position,Description,PricePerHour")] Occupation occupation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(occupation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(occupation);
        }

        [Authorize(Roles = "Admin")]
        // GET: Occupations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = await _context.Occupations.FindAsync(id);
            if (occupation == null)
            {
                return NotFound();
            }
            return View(occupation);
        }

        // POST: Occupations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Position,Description,PricePerHour")] Occupation occupation)
        {
            if (id != occupation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(occupation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OccupationExists(occupation.ID))
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
            return View(occupation);
        }

        [Authorize(Roles = "Admin")]
        // GET: Occupations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var occupation = await _context.Occupations
                .FirstOrDefaultAsync(m => m.ID == id);
            if (occupation == null)
            {
                return NotFound();
            }

            return View(occupation);
        }

        [Authorize(Roles = "Admin")]
        // POST: Occupations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var occupation = await _context.Occupations.FindAsync(id);
            _context.Occupations.Remove(occupation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OccupationExists(int id)
        {
            return _context.Occupations.Any(e => e.ID == id);
        }
    }
}
