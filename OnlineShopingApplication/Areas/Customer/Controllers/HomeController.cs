using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopingApplication.Data;
using OnlineShopingApplication.Models;

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
}
