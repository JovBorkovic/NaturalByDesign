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
    public class MaterialsController : Controller
    {
        private readonly NbdContext _context;

        public MaterialsController(NbdContext context)
        {
            _context = context;
        }

        // GET: Materials
        public async Task<IActionResult> Index(string sortMaterials, string searchString)
        {
                ViewData["materialName"] = String.IsNullOrEmpty(sortMaterials) ? "materialName_desc" : "materialName";
                ViewData["SearchFilter"] = searchString;

                var materials = from m in _context.Materials
                                  select m;
                if (!String.IsNullOrEmpty(searchString))
                {
                    materials = materials.Where(m => m.MaterialName.Contains(searchString));
                }
                switch (sortMaterials)
                {
                    case "materialName":
                        materials = materials.OrderBy(m => m.MaterialName);
                        break;
                    case "materialName_desc":
                        materials = materials.OrderByDescending(m => m.MaterialName);
                        break;
                    default:
                        materials = materials.OrderBy(m => m.MaterialName);
                        break;
                }



                return View(await materials.AsNoTracking().ToListAsync());
            }

        // GET: Materials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Materials
                //.Include(m => m.Bids)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // GET: Materials/Create
        [Authorize(Roles = "Admin, Manager")]
        public IActionResult Create()
        {
            //ViewData["BidsID"] = new SelectList(_context.Bids, "ID", "ID");
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,MaterialName")] Material material)
        {
            if (ModelState.IsValid)
            {
                _context.Add(material);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["BidsID"] = new SelectList(_context.Bids, "ID", "ID", material.BidsID);
            return View(material);
        }

        // GET: Materials/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            //ViewData["BidsID"] = new SelectList(_context.Bids, "ID", "ID", material.BidsID);
            return View(material);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,MaterialName")] Material material)
        {
            if (id != material.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(material);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialExists(material.ID))
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
            //ViewData["BidsID"] = new SelectList(_context.Bids, "ID", "ID", material.BidsID);
            return View(material);
        }

        // GET: Materials/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = await _context.Materials
                //.Include(m => m.Bids)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaterialExists(int id)
        {
            return _context.Materials.Any(e => e.ID == id);
        }
    }
}
