using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payroll.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public TaskItemController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
        {
            var items = await _unitOfWork.TaskRepo.GetAllsAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TaskItem>> Get(int id)
        {
            var item = await _unitOfWork.TaskRepo.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItem>> Create([FromBody] TaskItem task)
        {
            if (task == null) return BadRequest();
            await _unitOfWork.TaskRepo.AddAsync(task);
            await _unitOfWork.TaskRepo.SaveAsync();
            return CreatedAtAction(nameof(Get), new { id = task.TaskId }, task);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskItem task)
        {
            if (task == null || id != task.TaskId) return BadRequest();
            var existing = await _unitOfWork.TaskRepo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Category = task.Category;
            existing.Name = task.Name;

            _unitOfWork.TaskRepo.Update(existing);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _unitOfWork.TaskRepo.GetByIdAsync(id);
            if (existing == null) return NotFound();
            _unitOfWork.TaskRepo.Delete(existing);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}