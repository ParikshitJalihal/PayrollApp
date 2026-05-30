using Microsoft.AspNetCore.Mvc;

namespace Payroll.API.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
