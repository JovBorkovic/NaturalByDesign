using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NbdAplication.Data;
using NbdAplication.Models;
using NbdAplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using NbdAplication.Utilities;

namespace NbdAplication.Controllers
{
    [Authorize(Roles = "Admin, Sales, Production, Manager, Botanist, Designer")]
    public class BidsController : Controller
    {
        private readonly NbdContext _context;

        private bool isAdmin = false;

        public BidsController(NbdContext context)
        {
            _context = context;
        }

        // GET: Bids
        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortBid, string SearchString, int? page, int? pageSizeID)
        {
            ViewData["ProjectNameSortParm"] = String.IsNullOrEmpty(sortBid) ? "projectname_desc" : "";
            ViewData["estAmount"] = sortBid == "estAmount" ? "estAmount_desc" : "estAmount";
            ViewData["actAmount"] = sortBid == "actAmount" ? "actAmount_desc" : "actAmount";
            ViewData["SearchFilter"] = SearchString;

            var bids = from b in _context.Bids.Include(p => p.Projects).Include(i => i.BidInventories).ThenInclude(i => i.Inventory)
                       select b;
            if (!String.IsNullOrEmpty(SearchString))
            {
                bids = bids.Where(b => b.Projects.PName.Contains(SearchString)
                                        || b.Projects.PSite.Contains(SearchString));
                page = 1;
            }

            switch (sortBid)
            {
                case "projectname_desc":
                    bids = bids.OrderByDescending(b => b.Projects.PName);
                    break;
                case "estAmount":
                    bids = bids.OrderBy(b => b.EstAmount);
                    break;
                case "estAmount_desc":
                    bids = bids.OrderByDescending(b => b.EstAmount);
                    break;
                case "actAmount":
                    bids = bids.OrderBy(b => b.ActAmount);
                    break;
                case "actAmount_desc":
                    bids = bids.OrderByDescending(b => b.ActAmount);
                    break;
                default:
                    bids = bids.OrderBy(b => b.Projects.PName);
                    break;
            }

            int pageSize;
            if (pageSizeID.HasValue)
            {
                pageSize = pageSizeID.GetValueOrDefault();
                CookieOptionsHelper.CookieSet(HttpContext, "pageSizeValue", pageSize.ToString(), 30);
            }
            else
            {
                pageSize = Convert.ToInt32(HttpContext.Request.Cookies["pageSizeValue"]);
            }
            pageSize = (pageSize == 0) ? 3 : pageSize;
            ViewData["pageSizeID"] =
                new SelectList(new[] { "3", "5", "15", "30" }, pageSize.ToString());
            var pagedData = await PaginatedList<Bids>.CreateAsync(bids.AsNoTracking(), page ?? 1, pageSize);


            return View(pagedData);
        }

        // GET: Bids/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bids = await _context.Bids
                .Include(b => b.Designer)
                .Include(b => b.Nbdstaff)
                .Include(b => b.Occupation)
                .Include(b => b.Projects)
                .Include(b => b.Sale)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bids == null)
            {
                return NotFound();
            }

            return View(bids);
        }

        
        // GET: Bids/Create
        [Authorize(Roles = "Admin, Manager, Sales")]
        public IActionResult Create()
        {
            ViewData["DesignerID"] = new SelectList(_context.Set<Designer>(), "ID", "Designers");
            ViewData["NbdstaffID"] = new SelectList(_context.Nbdstaffs, "ID", "FullName");
            ViewData["OccupationID"] = new SelectList(_context.Occupations, "ID", "Position");
            ViewData["ProjectsID"] = new SelectList(_context.Projects, "ID", "PName");
            ViewData["SaleID"] = new SelectList(_context.Set<Sale>(), "ID", "Sales");

            var bids = new Bids();
            PopulateAssignedInventoryData(bids);
            return View();
        }

        // POST: Bids/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,EstAmount,ActAmount,IsApprovedByClient,IsApprovedByNBD,RevisionCheck,ProjectsID,NbdstaffID,OccupationID,SaleID,DesignerID")] Bids bids, string[] selectedOptions)
        {

            //Add the selected inventory
            if (selectedOptions != null)
            {
                foreach (var inventory in selectedOptions)
                {
                   var inventoryToAdd = new BidInventory { BidsID = bids.ID, InventoryID = int.Parse(inventory) };
                   bids.BidInventories.Add(inventoryToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(bids);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DesignerID"] = new SelectList(_context.Set<Designer>(), "ID", "Designers", bids.DesignerID);
            ViewData["NbdstaffID"] = new SelectList(_context.Nbdstaffs, "ID", "FullName", bids.NbdstaffID);
            ViewData["OccupationID"] = new SelectList(_context.Occupations, "ID", "Position", bids.OccupationID);
            ViewData["ProjectsID"] = new SelectList(_context.Projects, "ID", "PName", bids.ProjectsID);
            ViewData["SaleID"] = new SelectList(_context.Set<Sale>(), "ID", "Sales", bids.SaleID);

            PopulateAssignedInventoryData(bids);
            return View(bids);
        }

        // GET: Bids/Edit/5
        [Authorize(Roles = "Admin, Manager, Sales")]
        public async Task<IActionResult> Edit(int? id)
        {

            ViewBag.isAdmin = false;

            if (id == null)
            {
                return NotFound();
            }

            //var bids = await _context.Bids.FindAsync(id);
            var bids = await _context.Bids
                .Include(p => p.BidInventories).ThenInclude(p => p.Inventory)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ID == id);


            ViewData["DesignerID"] = new SelectList(_context.Set<Designer>(), "ID", "Designers", bids.DesignerID);
            ViewData["NbdstaffID"] = new SelectList(_context.Nbdstaffs, "ID", "FullName", bids.NbdstaffID);
            ViewData["OccupationID"] = new SelectList(_context.Occupations, "ID", "Position", bids.OccupationID);
            ViewData["ProjectsID"] = new SelectList(_context.Projects, "ID", "PName", bids.ProjectsID);
            ViewData["SaleID"] = new SelectList(_context.Set<Sale>(), "ID", "Sales", bids.SaleID);

            if (User.IsInRole("Admin"))
            {
                ViewBag.isAdmin = true;
            }

            PopulateAssignedInventoryData(bids);
            return View(bids);
        }

        // POST: Bids/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, Manager, Sales")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("ID,EstAmount,ActAmount,IsApprovedByClient,IsApprovedByNBD,RevisionCheck,ProjectsID,NbdstaffID,OccupationID,SaleID,DesignerID")] Bids bids,
            string[] selectedOptions)
        {

            if (id != bids.ID)
            {
                return NotFound();
            }

            //var bidToUpdate = await _context.Bids
            //    //.Include(b => b.ActAmount)
            //    //.Include(b => b.EstAmount)
            //    //.Include(b => b.IsApprovedByClient)
            //    //.Include(b => b.IsApprovedByNBD)
            //    //.Include(b => b.RevisionCheck)
            //    .Include(b => b.Projects)
            //    .Include(b => b.Nbdstaff)
            //    .Include(b => b.BidInventories).ThenInclude(i => i.Inventory)
            //    .AsTracking()
            //    .FirstOrDefaultAsync(b => b.ID == id);

            //if (bidToUpdate == null)
            //{
            //    return NotFound();
            //}


            UpdateBidInventories(selectedOptions, bids);

            //if (await TryUpdateModelAsync<Bids>(
            //    bids,
            //    "",
            //    p => p.EstAmount, p => p.ActAmount, p => p.IsApprovedByClient, p => p.IsApprovedByNBD,
            //    p => p.RevisionCheck, p => p.ProjectsID, p => p.NbdstaffID, p => p.OccupationID, p => p.SaleID,
            //    p => p.DesignerID, p => p.BidInventories))
            //{
                if (ModelState.IsValid)
                        {
                            try
                            {
                                _context.Update(bids);
                                await _context.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException ex)
                            {
                                if (!BidsExists(bids.ID))
                                {
                                    ModelState.AddModelError("", "Unable to save changes." + ex.Message.ToString());
                                    return NotFound();
                                }
                                else
                                {
                                    throw;
                                }
                            }
                            return RedirectToAction(nameof(Index));
                        }
            //}
           


                
            ViewData["DesignerID"] = new SelectList(_context.Set<Designer>(), "ID", "Designers", bids.DesignerID);
            ViewData["NbdstaffID"] = new SelectList(_context.Nbdstaffs, "ID", "FullName", bids.NbdstaffID);
            ViewData["OccupationID"] = new SelectList(_context.Occupations, "ID", "Position", bids.OccupationID);
            ViewData["ProjectsID"] = new SelectList(_context.Projects, "ID", "PName", bids.ProjectsID);
            ViewData["SaleID"] = new SelectList(_context.Set<Sale>(), "ID", "Sales", bids.SaleID);

            PopulateAssignedInventoryData(bids);
            return View(bids);

        }

        [Authorize(Roles = "Admin")]
        // GET: Bids/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bids = await _context.Bids
                .Include(b => b.Designer)
                .Include(b => b.Nbdstaff)
                .Include(b => b.Occupation)
                .Include(b => b.Projects)
                .Include(b => b.Sale)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (bids == null)
            {
                return NotFound();
            }

            ViewBag.isAdmin = true;

            return View(bids);
        }

        // POST: Bids/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bids = await _context.Bids.FindAsync(id);
            _context.Bids.Remove(bids);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void PopulateAssignedInventoryData(Bids bids)
        {
            var allOptions = _context.Inventories;
            var currentOptionIDs = new HashSet<int>(bids.BidInventories.Select(b => b.InventoryID));
            var checkBoxes = new List<OptionVM>();
            foreach (var option in allOptions)
            {
                checkBoxes.Add(new OptionVM
                {
                    ID = option.ID,
                    DisplayText = option.ItemDescription,
                    Assigned = currentOptionIDs.Contains(option.ID)
                });
            }
            ViewData["ConditionOptions"] = checkBoxes;
        }
        private void UpdateBidInventories(string[] selectedOptions, Bids bidToUpdate)
        {
            if (selectedOptions == null)
            {
                bidToUpdate.BidInventories = new List<BidInventory>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var bidOptionsHS = new HashSet<int>
                (bidToUpdate.BidInventories.Select(i => i.InventoryID));//IDs of the currently selected inventory
            foreach (var option in _context.Inventories)
            {
                if (selectedOptionsHS.Contains(option.ID.ToString()))
                {
                    if (!bidOptionsHS.Contains(option.ID))
                    {
                        bidToUpdate.BidInventories.Add(new BidInventory { BidsID = bidToUpdate.ID, InventoryID = option.ID });
                        //bidToUpdate.BidInventories.Sum(new BidInventory { BidsID = bidToUpdate.ID, InventoryID = option.List});
                    }
                }
                else
                {
                    if (bidOptionsHS.Contains(option.ID))
                    {
                        BidInventory inventoryToRemove = bidToUpdate.BidInventories.SingleOrDefault(c => c.InventoryID == option.ID);
                        _context.Remove(inventoryToRemove);
                    }
                }
            }
        }

        private bool BidsExists(int id)
        {
            return _context.Bids.Any(e => e.ID == id);
        }
    }
}
