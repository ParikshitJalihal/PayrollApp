using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;

namespace Payroll.API.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class PayComponentController : ControllerBase
    {
        private readonly IUnitOfWork _db;
        public PayComponentController(IUnitOfWork db) => _db = db;

        [HttpGet]
        public ActionResult<IEnumerable<PayComponent>> GetAll()
        {
            var list = _db.ComponentRepository.GetAll().ToList();
            return Ok(list);
        }

        [HttpPost]
        public IActionResult Upsert([FromBody] PayComponent dto)
        {
            if (dto.PayComponentId == 0)
                _db.ComponentRepository.Add(dto);
            else
                _db.ComponentRepository.Update(dto);

            _db.Save();
            return Ok(dto);
        }

        [HttpGet("employee-pays")]
        public IActionResult GetEmployeePayComponent()
        {
            var list = _db.EmployeePayRepository.GetAll().ToList();
            return Ok(list);
        }

        [HttpGet("employee/{employeeId:int}/pays")]
        public async Task<ActionResult<IEnumerable<PayComponentModel>>> GetEmployeePays(int employeeId)
        {
            var allPays = _db.EmployeePayRepository.GetAll(includeProperties: "PayComponent");
            var result = allPays
                .Where(e => e.EmployeeId == employeeId)
                .Select(ep => new PayComponentModel
                {
                    PayComponentId = ep.PayComponent?.PayComponentId ?? 0,
                    ComponentName = ep.PayComponent?.ComponentName,
                    ComponentType = ep.PayComponent?.ComponentType,
                    PayCode = ep.PayComponent?.PayTypeCode,
                    MonthlyAmount = ep.MonthlyAmount,
                    AnnualAmount = ep.AnnualAmount,
                    EffectiveDate = ep.EffectiveDate,
                    EffectiveEndDate = Convert.ToDateTime(ep.EffectiveEndDate),
                    CompanyId = 0
                })
                .ToList();

            return Ok(result);
        }
    }

}
