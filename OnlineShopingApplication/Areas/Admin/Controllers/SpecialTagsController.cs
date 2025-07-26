using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using OnlineShopingApplication.Data;
using OnlineShopingApplication.Models;

namespace OnlineShopingApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SpecialTagsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SpecialTagsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.SpecialTags.ToList();
            return View(result);
        }
        // Create Http Get Action Method
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        // Create Http Post Action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SpecialTags specialTags)
        {
            if (ModelState.IsValid)
            {
                _context.SpecialTags.Add(specialTags);
                TempData["SuccessMessage"] = "Special Tags created successfully!";
                //_context.Add(specialTags);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "SpecialTags");

            }
            return View(specialTags);
        }
        //Edit Http Get Action Method
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTags = await _context.SpecialTags.FindAsync(id);
            if (specialTags == null)
            {
                return NotFound();
            }
            return View(specialTags);
        }
        // Edit Http Post Action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SpecialTags specialTags)
        {
            if (id != specialTags.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.SpecialTags.Update(specialTags);
                TempData["SuccessMessage"] = "Special Tags Updated successfully!";
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "SpecialTags");

            }
            return View(specialTags);
        }
        //Details Http Get Action Method
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTags = await _context.SpecialTags.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            if (specialTags == null)
            {
                return NotFound();
            }
            return View(specialTags);
        }
        //For Details there is no need post Action method 


        //Delete Http Get Action Method
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var specialTags = await _context.SpecialTags.FindAsync(id);
            if (specialTags == null)
            {
                return NotFound();
            }
            return View(specialTags);
        }
        // Delete Http Post Action method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {

            SpecialTags specialTags = await _context.SpecialTags.FindAsync(id);
            if (specialTags == null)
            {
                return NotFound();
            }

            _context.SpecialTags.Remove(specialTags);
            TempData["DeleteSuccessMessage"] = "Product Delete successfully!";
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "SpecialTags");

        }
    }
}
