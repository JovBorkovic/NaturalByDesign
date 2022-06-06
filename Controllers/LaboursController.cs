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
    [Authorize]
    public class LaboursController : Controller
    {
        private readonly NbdContext _context;

        public LaboursController(NbdContext context)
        {
            _context = context;
        }

        // GET: Labours
        public async Task<IActionResult> Index()
        {
            var nbdContext = _context.Labours.Include(l => l.Bids).Include(l => l.Nbdstaff).Include(l => l.Occupation).Include(l => l.Task);
            return View(await nbdContext.ToListAsync());
        }

        // GET: Labours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labour = await _context.Labours
                .Include(l => l.Bids)
                .Include(l => l.Nbdstaff)
                .Include(l => l.Occupation)
                .Include(l => l.Task)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (labour == null)
            {
                return NotFound();
            }

            return View(labour);
        }

        // GET: Labours/Create
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Create()
        {
            ViewData["BidsID"] = new SelectList(_context.Bids, "ID", "ID");
            ViewData["NbdstaffID"] = new SelectList(_context.Nbdstaffs, "ID", "FullName");
            ViewData["OccupationID"] = new SelectList(_context.Occupations, "ID", "Position");
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "TaskDescription");
            return View();
        }

        // POST: Labours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Hours,LabourDesc,NbdstaffID,BidsID,TaskID,OccupationID")] Labour labour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BidsID"] = new SelectList(_context.Bids, "ID", "ID", labour.BidsID);
            ViewData["NbdstaffID"] = new SelectList(_context.Nbdstaffs, "ID", "FullName", labour.NbdstaffID);
            ViewData["OccupationID"] = new SelectList(_context.Occupations, "ID", "Position", labour.OccupationID);
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "TaskDescription", labour.TaskID);
            return View(labour);
        }

        // GET: Labours/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labour = await _context.Labours.FindAsync(id);
            if (labour == null)
            {
                return NotFound();
            }
            ViewData["BidsID"] = new SelectList(_context.Bids, "ID", "ID", labour.BidsID);
            ViewData["NbdstaffID"] = new SelectList(_context.Nbdstaffs, "ID", "FullName", labour.NbdstaffID);
            ViewData["OccupationID"] = new SelectList(_context.Occupations, "ID", "Position", labour.OccupationID);
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "TaskDescription", labour.TaskID);
            return View(labour);
        }

        // POST: Labours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Hours,LabourDesc,NbdstaffID,BidsID,TaskID,OccupationID")] Labour labour)
        {
            if (id != labour.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabourExists(labour.ID))
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
            ViewData["BidsID"] = new SelectList(_context.Bids, "ID", "ID", labour.BidsID);
            ViewData["NbdstaffID"] = new SelectList(_context.Nbdstaffs, "ID", "FullName", labour.NbdstaffID);
            ViewData["OccupationID"] = new SelectList(_context.Occupations, "ID", "Position", labour.OccupationID);
            ViewData["TaskID"] = new SelectList(_context.Tasks, "ID", "TaskDescription", labour.TaskID);
            return View(labour);
        }

        // GET: Labours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labour = await _context.Labours
                .Include(l => l.Bids)
                .Include(l => l.Nbdstaff)
                .Include(l => l.Occupation)
                .Include(l => l.Task)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (labour == null)
            {
                return NotFound();
            }

            return View(labour);
        }

        // POST: Labours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labour = await _context.Labours.FindAsync(id);
            _context.Labours.Remove(labour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabourExists(int id)
        {
            return _context.Labours.Any(e => e.ID == id);
        }
    }
}
