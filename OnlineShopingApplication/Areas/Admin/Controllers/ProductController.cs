using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShopingApplication.Data;
using OnlineShopingApplication.Models;
using static System.Collections.Specialized.BitVector32;

namespace OnlineShopingApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {

            var Product = _context.Products.Include(p => p.ProductTypes).Include(q => q.SpecialTags).ToList();
            return View(Product);
        }

        //Create Http Get Action Method
      
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ProductTypesId = new SelectList(_context.ProductTypes.ToList(), "Id", "ProductType");
            ViewBag.SpecialTagsId = new SelectList(_context.SpecialTags.ToList(), "Id", "SpecialTagName");
            return View();
        }

        // Create Http Post Action method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Products products, IFormFile? image)
        {

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var fileName = Path.Combine(_webHostEnvironment.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(fileName, FileMode.Create));
                    products.Image = "Images/" + Path.GetFileName(image.FileName);
                }
                if(image == null)
                {
                    products.Image = "Images/No image.png";
                }
              
                _context.Products.Add(products);
                TempData["SuccessMessage"] = "Product created successfully!"; await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Product");

            }

            //foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            //{
            //    Console.WriteLine(error.ErrorMessage);
            //}

            ViewBag.ProductTypesId = new SelectList(_context.ProductTypes.ToList(), "Id", "ProductType");
            ViewBag.SpecialTagsId = new SelectList(_context.SpecialTags.ToList(), "Id", "SpecialTagName");
            return View(products);
        }
    }
}
