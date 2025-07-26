using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopingApplication.Data;
using OnlineShopingApplication.Models;

namespace OnlineShopingApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _context;
        public ProductTypesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.ProductTypes.ToList();
            return View(result);
        }

        //Create Http Get Action Method
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        // Create Http Post Action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid)
            {
                _context.ProductTypes.Add(productTypes);
                TempData["SuccessMessage"] = "Product created successfully!";
                //_context.Add(productTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "ProductTypes");

            }
            return View(productTypes);
        }
        //Edit Http Get Action Method
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var ProductType = await _context.ProductTypes.FindAsync(id);
            if (ProductType == null)
            {
                return NotFound();
            }
            return View(ProductType);
        }
        // Edit Http Post Action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,ProductTypes productTypes)
        {
            if(id != productTypes.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _context.ProductTypes.Update(productTypes);
                TempData["SuccessMessage"] = "Product Updated successfully!";
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "ProductTypes");

            }
            return View(productTypes);
        }
        //Details Http Get Action Method
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ProductType = await _context.ProductTypes.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
            if (ProductType == null)
            {
                return NotFound();
            }
            return View(ProductType);
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
            var ProductType = await _context.ProductTypes.FindAsync(id);
            if (ProductType == null)
            {
                return NotFound();
            }
            return View(ProductType);
        }
        // Delete Http Post Action method
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
           
            ProductTypes productTypes = await _context.ProductTypes.FindAsync(id);
            if (productTypes == null)
            {
                return NotFound();
            }
            
            _context.ProductTypes.Remove(productTypes);
            TempData["DeleteSuccessMessage"] = "Product Delete successfully!";
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "ProductTypes");    
           
        }


    }
}
