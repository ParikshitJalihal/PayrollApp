


using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payroll.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            var items = await _unitOfWork.Employee.GetAllsAsync();
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> Get(int id)
        {
            var item = _unitOfWork.Employee.Get(i => i.EmployeeId == id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Create([FromBody] Employee employee)
        {
            if (employee == null) return BadRequest("Employee payload required.");

            // Validate and sanitize any embedded EmployeePayments before saving
            var (ok, error) = await ValidateAndSanitizePayments(employee);
            if (!ok)
                return BadRequest(error);

            try
            {
                await _unitOfWork.Employee.AddAsync(employee);
                await _unitOfWork.Employee.SaveAsync();

                return CreatedAtAction(nameof(Get), new { id = employee.EmployeeId }, employee);
            }
            catch (Exception ex)
            {
                // In development you can return more details; keep this safe for production
                return Problem(detail: ex.Message, statusCode: 500);
            }
        }



        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Employee employee)
        {
            if (employee == null || id != employee.EmployeeId) return BadRequest();
            var existing = await _unitOfWork.Employee.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.EmployeeName = employee.EmployeeName;
            existing.Email = employee.Email;
            existing.Phone = employee.Phone;
            existing.Department = employee.Department;
            existing.JobId = employee.JobId;

            _unitOfWork.Employee.Update(existing);
            _unitOfWork.Save();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _unitOfWork.Employee.GetByIdAsync(id);
            if (existing == null) return NotFound();
            _unitOfWork.Employee.Delete(existing);
            _unitOfWork.Save();
            return NoContent();
        }

        // Optional: onboard a candidate into an employee
        [HttpPost("onboard/{candidateId:int}")]
        public async Task<IActionResult> OnboardFromCandidate(int candidateId)
        {
            var candidate = await _unitOfWork.Candidate.GetByIdAsync(candidateId);
            if (candidate == null || !candidate.OnboardingRequested) return BadRequest();

            var employee = new Employee
            {
                EmployeeName = candidate.CadidateName,
                Email = candidate.CandidateEmail,
                Phone = candidate.CandidatePhone,
                Gender = candidate.CandidateGender,
                Department = candidate.JobDescription,
                JobId = candidate.JobId,
                JoiningDate = System.DateTime.Now,
                EmployeeCode = $"AUTO-{candidate.CandidateId}",
                CandidateId = candidate.CandidateId
            };

            await _unitOfWork.Employee.AddAsync(employee);
            await _unitOfWork.Employee.SaveAsync();

            // you may want to update candidate state here via _unitOfWork.Candidate.Update(...) and Save()

            return CreatedAtAction(nameof(Get), new { id = employee.EmployeeId }, employee);
        }

        private async Task<(bool Ok, string? Error)> ValidateAndSanitizePayments(Employee employee)
        {
            if (employee.EmployeePayments == null || !employee.EmployeePayments.Any())
                return (true, null);

            foreach (var ep in employee.EmployeePayments)
            {
                if (ep == null)
                    return (false, "Invalid EmployeePayment entry.");

                if (ep.PayComponentId == 0)
                    return (false, "EmployeePayment.PayComponentId is required and cannot be 0.");

                var component = _unitOfWork.ComponentRepository.Get(e => e.PayComponentId == ep.PayComponentId);
                if (component == null)
                    return (false, $"Referenced PayComponent with id {ep.PayComponentId} does not exist.");

                // Prevent EF from attempting to insert/merge the PayComponent navigation property
                ep.PayComponent = null;

                // Ensure required metadata defaults
                ep.CreatedDate = ep.CreatedDate == default ? DateTime.UtcNow : ep.CreatedDate;
            }

            return (true, null);
        }

    }
}