using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HCMWebApp.Areas.Admin.Controllers
{
    public class PayrollController : Controller
    {
        [HttpPost]
        public IActionResult RunPayroll(int employeeId, DateTime periodStart, DateTime periodEnd)
        {
            //var engine = new PayrollEngine(_context);
            //var result = engine.ProcessPayroll(employeeId, periodStart, periodEnd);

            return View("PayrollSummary","");
        }

    }
}
