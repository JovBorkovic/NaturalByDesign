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
    [Authorize(Roles = "Admin, Sales, Manager")]
    public class ClientsController : Controller
    {
        private readonly NbdContext _context;

        public ClientsController(NbdContext context)
        {
            _context = context;
        }

        // GET: Clients
        public async Task<IActionResult> Index(string sortClients, string searchString)
        {
            ViewData["ClientSortParm"] = String.IsNullOrEmpty(sortClients) ? "client_desc" : "Client";
            ViewData["ContactSortFirst"] = sortClients == "ContactFirst" ? "ContactFirst_desc" : "ContactFirst";
            ViewData["ContactSortLast"] = sortClients == "ContactLast" ? "ContactLast_desc" : "ContactLast";
            ViewData["SearchFilter"] = searchString;

            var clients = from c in _context.Client
                          select c;
            if (!String.IsNullOrEmpty(searchString))
            {
                clients = clients.Where(c => c.ClientName.Contains(searchString)
                                        || c.ContactFirst.Contains(searchString)
                                        || c.ContactLast.Contains(searchString)
                                        || c.ContactPhone.Contains(searchString));
            }
            switch (sortClients)
            {
                case "Client":
                    clients = clients.OrderBy(c => c.ClientName);
                    break;
                case "client_desc":
                    clients = clients.OrderByDescending(c => c.ClientName);
                    break;
                case "ContactFirst":
                    clients = clients.OrderBy(c => c.ContactFirst);
                    break;
                case "ContactFirst_desc":
                    clients = clients.OrderByDescending(c => c.ContactFirst);
                    break;
                case "ContactLast":
                    clients = clients.OrderBy(c => c.ContactLast);
                    break;
                case "ContactLast_desc":
                    clients = clients.OrderByDescending(c => c.ContactLast);
                    break;
                default:
                    clients = clients.OrderBy(c => c.ClientName);
                    break;
            }

            return View(await clients.AsNoTracking().ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }


        [Authorize(Roles = "Sales, Admin, Manager")]
        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,ClientName,ContactFirst,ContactLast,ContactPos,Address,ContactEmail,ContactPhone")] Client client)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        [Authorize(Roles = "Sales, Admin, Manager")]
        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,ClientName,ContactFirst,ContactLast,ContactPos,Address,ContactEmail,ContactPhone")] Client client)
        {
            if (id != client.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.ClientId))
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
            return View(client);
        }

        [Authorize(Roles = "Admin")]
        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return _context.Client.Any(e => e.ClientId == id);
        }
    }
}
