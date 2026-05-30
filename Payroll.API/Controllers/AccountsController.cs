using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payroll.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAll()
        {
            var accounts = await _unitOfWork.Account.GetAllsAsync();
            return Ok(accounts);
        }

        // GET: api/Accounts/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Account>> GetById(int id)
        {
            var account = await _unitOfWork.Account.GetByIdAsync(id);
            if (account == null) return NotFound();
            return Ok(account);
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<ActionResult<Account>> Create([FromBody] Account account)
        {
            if (account == null) return BadRequest();

            await _unitOfWork.Account.AddAsync(account);
            await _unitOfWork.Account.SaveAsync();

            // return 201 with location header pointing to GET by id
            return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
        }

        // PUT: api/Accounts/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Account account)
        {
            if (account == null || id != account.Id) return BadRequest();

            var existing = await _unitOfWork.Account.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // update fields (simple assignment; adapt if you want DTOs or mapping)
            existing.AccountName = account.AccountName;
            existing.AccountType = account.AccountType;

            _unitOfWork.Account.Update(existing);
            _unitOfWork.Save();

            return NoContent();
        }

        // DELETE: api/Accounts/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _unitOfWork.Account.GetByIdAsync(id);
            if (account == null) return NotFound();

            _unitOfWork.Account.Delete(account);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}