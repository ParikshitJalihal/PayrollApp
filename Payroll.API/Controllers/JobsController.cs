
using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payroll.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobsController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Jobs>>> GetAll()
        {
            var items = await _unitOfWork.Jobs.GetAllsAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Jobs>> Get(int id)
        {
            var item = await _unitOfWork.Jobs.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Jobs>> Create([FromBody] Jobs jobs)
        {
            if (jobs == null) return BadRequest();
            await _unitOfWork.Jobs.AddAsync(jobs);
            await _unitOfWork.Jobs.SaveAsync();
            return CreatedAtAction(nameof(Get), new { id = jobs.JobId }, jobs);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Jobs jobs)
        {
            if (jobs == null || id != jobs.JobId) return BadRequest();
            var existing = await _unitOfWork.Jobs.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.JobName = jobs.JobName;
            existing.JobStatusDescription = jobs.JobStatusDescription;

            _unitOfWork.Jobs.Update(existing);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _unitOfWork.Jobs.GetByIdAsync(id);
            if (existing == null) return NotFound();
            _unitOfWork.Jobs.Delete(existing);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}