using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
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
    }
    
}
