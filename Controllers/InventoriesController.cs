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
    public class InventoriesController : Controller
    {
        private readonly NbdContext _context;

        public InventoriesController(NbdContext context)
        {
            _context = context;
        }

        // GET: Inventories
        public async Task<IActionResult> Index()
        {
            var nbdContext = _context.Inventories;
            return View(await nbdContext.ToListAsync());
        }

        // GET: Inventories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                //.Include(i => i.Bids)
                .Include(i => i.Material)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        [Authorize(Roles = "Admin, Manager")]
        // GET: Inventories/Create
        public IActionResult Create()
        {
            ViewData["BidsID"] = new SelectList(_context.Bids, "ID", "ID");
            ViewData["MaterialID"] = new SelectList(_context.Materials, "ID", "MaterialName");
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Manager, Sales, Production")]
        public async Task<IActionResult> Create([Bind("ID,Code,ItemDescription,List,Size,BidsID,MaterialID")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["BidsID"] = new SelectList(_context.Bids, "ID", "ID", inventory.BidsID);
            ViewData["MaterialID"] = new SelectList(_context.Materials, "ID", "MaterialName", inventory.MaterialID);
            return View(inventory);
        }

        // GET: Inventories/Edit/5
        [Authorize(Roles = "Admin, Manager, Sales, Production")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            //ViewData["BidsID"] = new SelectList(_context.Bids, "ID", "ID", inventory.BidsID);
            ViewData["MaterialID"] = new SelectList(_context.Materials, "ID", "MaterialName", inventory.MaterialID);
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Code,ItemDescription,List,Size,BidsID,MaterialID")] Inventory inventory)
        {
            if (id != inventory.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.ID))
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
            //ViewData["BidsID"] = new SelectList(_context.Bids, "ID", "ID", inventory.BidsID);
            ViewData["Code"] = new SelectList(_context.Inventories, "ID", "Code", inventory.Code);
            ViewData["MaterialID"] = new SelectList(_context.Materials, "ID", "MaterialName", inventory.MaterialID);
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                //.Include(i => i.Bids)
                .Include(i => i.Material)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventories.Any(e => e.ID == id);
        }
    }
}
