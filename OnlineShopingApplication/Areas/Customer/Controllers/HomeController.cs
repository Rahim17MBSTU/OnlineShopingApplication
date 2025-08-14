using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopingApplication.Data;
using OnlineShopingApplication.Models;
using OnlineShopingApplication.Utility;

namespace OnlineShopingApplication.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;
    
    public HomeController( ApplicationDbContext context,ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
        
    }
    
    public IActionResult Index()
    {
        var product = _context.Products.Include(p => p.ProductTypes).Include(p => p.SpecialTags).ToList();
        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    //Get product detail action method
    public ActionResult Details(int? id)
    {
        if(id == null)
        {
            return NotFound();
        }
        var product = _context.Products.Include(p => p.ProductTypes).FirstOrDefault(p=>p.Id == id);
        if(product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    [HttpPost]
    [ActionName("Details")]
    public ActionResult ProductDetails(int? id)
    {
        List<Products> products = new List<Products>();
        if (id == null)
        {
            return NotFound();
        }
        var product = _context.Products.Include(p => p.ProductTypes).FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        products = HttpContext.Session.Get<List<Products>>("products");
        if(products == null)
        {
            products = new List<Products>();
        }
        products.Add(product);
        HttpContext.Session.Set("products", products);
        return View(product);
    }
    [HttpPost]
    public IActionResult Remove(int? id)
    {
        List<Products> products = HttpContext.Session.Get<List<Products>>("products");
        if (products != null)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
            }
        }
        HttpContext.Session.Set("products", products);
        return RedirectToAction(nameof(Index));
    }

    //Get product cart action method
    public IActionResult Cart()
    {
        List<Products> products = HttpContext.Session.Get<List<Products>>("products");
        if(products == null)
        {
            products = new List<Products>();
        }

        return View(products);
    }
}
