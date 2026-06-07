using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Payroll.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        //private readonly IPayrollEngineService _payrollEngine;

        //public PayrollController(IPayrollEngineService payrollEngine)
        //{
        //    _payrollEngine = payrollEngine;
        //}

        //[HttpPost("run")]
        //public async Task<IActionResult> RunPayroll(int employeeId, DateTime start, DateTime end)
        //{
        //    var result = await _payrollEngine.ProcessPayrollAsync(employeeId, start, end);
        //    return Ok(result);
        //}
    }
}
