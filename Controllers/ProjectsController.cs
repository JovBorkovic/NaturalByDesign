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
    public class ProjectsController : Controller
    {
        private readonly NbdContext _context;

        public ProjectsController(NbdContext context)
        {
            _context = context;
        }

        // GET: Projects
        //public async Task<IActionResult> Index()
        //{
        //    var nbdContext = _context.Projects.Include(p => p.Client);
        //    return View(await nbdContext.ToListAsync());
        //}
        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortProjects, string searchString)
        {
            ViewData["ProjectNameSortParm"] = String.IsNullOrEmpty(sortProjects) ? "projectname_desc" : "";
            ViewData["ClientSortParm"] = sortProjects == "Client" ? "client_desc" : "Client";
            ViewData["SearchFilter"] = searchString;

            var projects = from p in _context.Projects
                           select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                projects = projects.Where(p => p.PName.Contains(searchString)
                                        || p.PSite.Contains(searchString));
            }
            switch (sortProjects)
            {
                case "projectname_desc":
                    projects = projects.OrderByDescending(p => p.PName);
                    break;
                case "Client":
                    projects = projects.OrderBy(p => p.Client);
                    break;
                case "client_desc":
                    projects = projects.OrderByDescending(p => p.Client);
                    break;
                default:
                    projects = projects.OrderBy(p => p.PName);
                    break;
            }
            return View(await projects.AsNoTracking().ToListAsync());

        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = await _context.Projects
                .Include(p => p.Client)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (projects == null)
            {
                return NotFound();
            }

            return View(projects);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ViewData["ClientID"] = new SelectList(_context.Client, "ClientId", "ClientName");
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,PName,EstStart,EstEnd,PSite,ActStartDate,ActEndDate,BidAmount,ClientID")] Projects projects)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projects);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientID"] = new SelectList(_context.Client, "ClientName", "Address", projects.ClientID);
            return View(projects);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = await _context.Projects.FindAsync(id);
            if (projects == null)
            {
                return NotFound();
            }
            ViewData["ClientID"] = new SelectList(_context.Client, "ClientId", "ClientName", projects.ClientID);
            return View(projects);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,PName,EstStart,EstEnd,PSite,ActStartDate,ActEndDate,BidAmount,ClientID")] Projects projects)
        {
            if (id != projects.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projects);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectsExists(projects.ID))
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
            ViewData["ClientID"] = new SelectList(_context.Client, "ClientId", "ClientName", projects.ClientID);
            return View(projects);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projects = await _context.Projects
                .Include(p => p.Client)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (projects == null)
            {
                return NotFound();
            }

            return View(projects);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projects = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(projects);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectsExists(int id)
        {
            return _context.Projects.Any(e => e.ID == id);
        }
    }
}
