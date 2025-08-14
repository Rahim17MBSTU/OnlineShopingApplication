using Microsoft.AspNetCore.Mvc;
using OnlineShopingApplication.Data;

namespace OnlineShopingApplication.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
