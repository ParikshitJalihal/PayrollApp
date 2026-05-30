using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeSheetEntryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TimeSheetEntryController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        // GET: api/TimeSheetEntry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeSheetEntry>>> GetAll()
        {
            var entries = (await _unitOfWork.TimeSheet.GetAllsAsync()).ToList();

            // populate Task navigation (best-effort)
            foreach (var e in entries)
            {
                if (e.TaskId != 0)
                    e.Task = await _unitOfWork.TaskRepo.GetByIdAsync(e.TaskId);
            }

            return Ok(entries);
        }

        // GET: api/TimeSheetEntry/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TimeSheetEntry>> GetById(int id)
        {
            var entry = await _unitOfWork.TimeSheet.GetByIdAsync(id);
            if (entry == null) return NotFound();

            if (entry.TaskId != 0)
                entry.Task = await _unitOfWork.TaskRepo.GetByIdAsync(entry.TaskId);

            return Ok(entry);
        }

        // POST: api/TimeSheetEntry
        [HttpPost]
        public async Task<ActionResult<TimeSheetEntry>> Create([FromBody] TimeSheetEntry dto)
        {
            if (dto == null) return BadRequest();

            dto.CreatedDate = dto.CreatedDate == default ? System.DateTime.UtcNow : dto.CreatedDate;

            await _unitOfWork.TimeSheet.AddAsync(dto);
            await _unitOfWork.TimeSheet.SaveAsync();

            return CreatedAtAction(nameof(GetById), new { id = dto.EntryId }, dto);
        }

        // PUT: api/TimeSheetEntry/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TimeSheetEntry dto)
        {
            if (dto == null || id != dto.EntryId) return BadRequest();

            var existing = await _unitOfWork.TimeSheet.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // map updatable fields
            existing.EntryDate = dto.EntryDate;
            existing.LogHours = dto.LogHours;
            existing.TaskId = dto.TaskId;
            existing.SubTask = dto.SubTask;
            existing.Description = dto.Description;
            existing.ModifiedDate = System.DateTime.UtcNow;

            _unitOfWork.TimeSheet.Update(existing);
            await _unitOfWork.TimeSheet.SaveAsync();

            return NoContent();
        }

        // DELETE: api/TimeSheetEntry/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _unitOfWork.TimeSheet.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _unitOfWork.TimeSheet.Delete(existing);
            await _unitOfWork.TimeSheet.SaveAsync();

            return NoContent();
        }
    }
}