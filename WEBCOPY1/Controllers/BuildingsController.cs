using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBCOPY1.Data;
using WEBCOPY1.Models;

namespace WEBCOPY1.Controllers
{
    public class BuildingsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public BuildingsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Building> Buildings { get; set; }
        // GET: Sellers
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? pageNumber)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.StreetSortParm = String.IsNullOrEmpty(sortOrder) ? "Street" : "";
            ViewBag.NumberSortParm = String.IsNullOrEmpty(sortOrder) ? "Number" : "";
            ViewBag.SquareSortParm = String.IsNullOrEmpty(sortOrder) ? "Square" : "";
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "Price" : "";
            ViewBag.YearOfCreationSortParm = String.IsNullOrEmpty(sortOrder) ? "YearOfCreation" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var buildings = from s in _context.Building
                            select s;

            if (!string.IsNullOrEmpty(searchString))
            {
                buildings = buildings.Where(s => s.Street.Contains(searchString) ||
                                                 s.Number.ToString().Contains(searchString) ||
                                                 s.Square.ToString().Contains(searchString) ||
                                                 s.Price.ToString().Contains(searchString) ||
                                                 s.YearOfCreation.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "Street":
                    buildings = buildings.OrderBy(s => s.Street);
                    break;
                case "Number":
                    buildings = buildings.OrderBy(s => s.Number);
                    break;
                case "Square":
                    buildings = buildings.OrderBy(s => s.Square);
                    break;
                case "Price":
                    buildings = buildings.OrderBy(s => s.Price);
                    break;
                default:
                    buildings = buildings.OrderBy(s => s.YearOfCreation);
                    break;
            }

            int pageSize = buildings.Count();
            return View(await PaginatedList<Building>.CreateAsync(buildings.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [Authorize(Roles = "Seller")]
        // GET: Building/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Building/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Street,Number,Square,Price,YearOfCreation")] Building building)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(building);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(building);
        }

        [Authorize(Roles = "Seller")]
        // GET: Building/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = await _context.Building.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }
            return View(building);
        }

        // POST: Building/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var buildingsToUpdate = await _context.Building.FirstOrDefaultAsync(s => s.Id == id);
            if (await TryUpdateModelAsync<Building>(
                buildingsToUpdate,
                "",
                s => s.Street, s => s.Number, s => s.Square, s => s.Price, s => s.YearOfCreation))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(buildingsToUpdate);
        }

        [Authorize(Roles = "Seller")]
        // GET: Building/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Building
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(student);
        }

        // POST: Building/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var building = await _context.Building.FindAsync(id);
            if (building == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _context.Building.Remove(building);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
    }
}
