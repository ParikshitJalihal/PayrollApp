
using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payroll.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LedgerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public LedgerController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LedgerEntry>>> GetAll()
        {
            var entries = await _unitOfWork.Ledger.GetAllEntriesAsync();
            return Ok(entries);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LedgerEntry>> Get(int id)
        {
            var entry = await _unitOfWork.Ledger.GetEntryByIdAsync(id);
            if (entry == null) return NotFound();
            return Ok(entry);
        }

        [HttpPost]
        public async Task<ActionResult<LedgerEntry>> Create([FromBody] LedgerEntry entry)
        {
            if (entry == null) return BadRequest();
            await _unitOfWork.Ledger.AddEntryAsync(entry);
            await _unitOfWork.Ledger.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = entry.Id }, entry);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] LedgerEntry entry)
        {
            if (entry == null || id != entry.Id) return BadRequest();
            var existing = await _unitOfWork.Ledger.GetEntryByIdAsync(id);
            if (existing == null) return NotFound();

            existing.Description = entry.Description;
            existing.Debit = entry.Debit;
            existing.Credit = entry.Credit;
            existing.AccountId = entry.AccountId;
            existing.EntryDate = entry.EntryDate;

            _unitOfWork.Ledger.Update(existing);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _unitOfWork.Ledger.GetEntryByIdAsync(id);
            if (existing == null) return NotFound();
            _unitOfWork.Ledger.Delete(existing);
            _unitOfWork.Save();
            return NoContent();
        }
    }
}