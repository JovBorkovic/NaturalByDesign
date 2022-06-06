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
    [Authorize(Roles = "Admin")]
    public class NbdstaffsController : Controller
    {
        private readonly NbdContext _context;

        public NbdstaffsController(NbdContext context)
        {
            _context = context;
        }

        // GET: Nbdstaffs
        public async Task<IActionResult> Index()
        {
            var nbdContext = _context.Nbdstaffs.Include(n => n.Designer).Include(n => n.Occupation).Include(n => n.Sale);
            return View(await nbdContext.ToListAsync());
        }

        // GET: Nbdstaffs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nbdstaff = await _context.Nbdstaffs
                .Include(n => n.Designer)
                .Include(n => n.Occupation)
                .Include(n => n.Sale)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nbdstaff == null)
            {
                return NotFound();
            }

            return View(nbdstaff);
        }

        // GET: Nbdstaffs/Create
        public IActionResult Create()
        {
            ViewData["DesignerID"] = new SelectList(_context.Set<Designer>(), "ID", "Designers");
            ViewData["OccupationID"] = new SelectList(_context.Occupations, "ID", "Position");
            ViewData["SaleID"] = new SelectList(_context.Set<Sale>(), "ID", "Sales");
            return View();
        }

        // POST: Nbdstaffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StaffFirst,StaffLast,UserName,Password,Phone,OccupationID,SaleID,DesignerID")] Nbdstaff nbdstaff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nbdstaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DesignerID"] = new SelectList(_context.Set<Designer>(), "ID", "Designers", nbdstaff.DesignerID);
            ViewData["OccupationID"] = new SelectList(_context.Occupations, "ID", "Position", nbdstaff.OccupationID);
            ViewData["SaleID"] = new SelectList(_context.Set<Sale>(), "ID", "Sales", nbdstaff.SaleID);
            return View(nbdstaff);
        }

        // GET: Nbdstaffs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nbdstaff = await _context.Nbdstaffs.FindAsync(id);
            if (nbdstaff == null)
            {
                return NotFound();
            }
            ViewData["DesignerID"] = new SelectList(_context.Set<Designer>(), "ID", "Designers", nbdstaff.DesignerID);
            ViewData["OccupationID"] = new SelectList(_context.Occupations, "ID", "Position", nbdstaff.OccupationID);
            ViewData["SaleID"] = new SelectList(_context.Set<Sale>(), "ID", "Sales", nbdstaff.SaleID);
            return View(nbdstaff);
        }

        // POST: Nbdstaffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StaffFirst,StaffLast,UserName,Password,Phone,OccupationID,SaleID,DesignerID")] Nbdstaff nbdstaff)
        {
            if (id != nbdstaff.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nbdstaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NbdstaffExists(nbdstaff.ID))
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
            ViewData["DesignerID"] = new SelectList(_context.Set<Designer>(), "ID", "Designers", nbdstaff.DesignerID);
            ViewData["OccupationID"] = new SelectList(_context.Occupations, "ID", "Position", nbdstaff.OccupationID);
            ViewData["SaleID"] = new SelectList(_context.Set<Sale>(), "ID", "Sales", nbdstaff.SaleID);
            return View(nbdstaff);
        }

        // GET: Nbdstaffs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nbdstaff = await _context.Nbdstaffs
                .Include(n => n.Designer)
                .Include(n => n.Occupation)
                .Include(n => n.Sale)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (nbdstaff == null)
            {
                return NotFound();
            }

            return View(nbdstaff);
        }

        // POST: Nbdstaffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nbdstaff = await _context.Nbdstaffs.FindAsync(id);
            _context.Nbdstaffs.Remove(nbdstaff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NbdstaffExists(int id)
        {
            return _context.Nbdstaffs.Any(e => e.ID == id);
        }
    }
}
