
using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
using System;
using System.Linq;

namespace Payroll.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _env;

        public CandidateController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _env = env;
        }

        // GET: api/Candidate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetAll()
        {
            var items = await _unitOfWork.Candidate.GetAllsAsync();
            return Ok(items);
        }

        // GET: api/Candidate/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Candidate>> GetById(int id)
        {
            var item = await _unitOfWork.Candidate.GetByIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/Candidate
        // Accepts JSON candidate object. For file upload use multipart/form-data endpoint below.
        [HttpPost]
        public async Task<ActionResult<Candidate>> Create([FromBody] Candidate candidate)
        {
            if (candidate == null) return BadRequest();

            candidate.ProfileCreatedDate ??= DateTime.UtcNow;
            await _unitOfWork.Candidate.AddAsync(candidate);
            await _unitOfWork.Candidate.SaveAsync();

            return CreatedAtAction(nameof(GetById), new { id = candidate.CandidateId }, candidate);
        }

        // POST: api/Candidate/upload
        // Accepts multipart/form-data with fields for candidate JSON properties and optional file named "file".
        [HttpPost("upload")]
        [RequestSizeLimit(10_000_000)] // ~10MB, adjust as needed
        public async Task<ActionResult<Candidate>> CreateWithFile([FromForm] Candidate candidate, IFormFile? file)
        {
            if (candidate == null) return BadRequest();

            if (file != null && file.Length > 0)
            {
                var uploadsDir = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "profiles");
                Directory.CreateDirectory(uploadsDir);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var savePath = Path.Combine(uploadsDir, fileName);

                await using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // store relative path for web access
                candidate.ProfilePath = $"/uploads/profiles/{fileName}";
            }

            candidate.ProfileCreatedDate ??= DateTime.UtcNow;
            await _unitOfWork.Candidate.AddAsync(candidate);
            await _unitOfWork.Candidate.SaveAsync();

            return CreatedAtAction(nameof(GetById), new { id = candidate.CandidateId }, candidate);
        }

        // PUT: api/Candidate/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Candidate candidate)
        {
            if (candidate == null || id != candidate.CandidateId) return BadRequest();

            var existing = await _unitOfWork.Candidate.GetByIdAsync(id);
            if (existing == null) return NotFound();

            // Map updatable fields
            existing.CadidateName = candidate.CadidateName;
            existing.CandidateEmail = candidate.CandidateEmail;
            existing.CandidatePhone = candidate.CandidatePhone;
            existing.CandidateAddress = candidate.CandidateAddress;
            existing.CandidateGender = candidate.CandidateGender;
            existing.SkillSets = candidate.SkillSets;
            existing.JobId = candidate.JobId;
            existing.JobDescription = candidate.JobDescription;
            existing.IsSelected = candidate.IsSelected;
            existing.IsModified = candidate.IsModified;
            existing.IsPreviouslyAttended = candidate.IsPreviouslyAttended;
            existing.IsUnattended = candidate.IsUnattended;
            existing.ProfilePath = candidate.ProfilePath ?? existing.ProfilePath;
            existing.OnboardingRequested = candidate.OnboardingRequested;
            existing.ProfileCreatedDate = candidate.ProfileCreatedDate ?? existing.ProfileCreatedDate;
            //existing.ModifiedDate = DateTime.UtcNow;

            _unitOfWork.Candidate.Update(existing);
            _unitOfWork.Save();

            return NoContent();
        }

        // DELETE: api/Candidate/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _unitOfWork.Candidate.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _unitOfWork.Candidate.Delete(existing);
            _unitOfWork.Save();

            // Optionally delete profile file
            if (!string.IsNullOrEmpty(existing.ProfilePath))
            {
                var fullPath = Path.Combine(_env.WebRootPath ?? "wwwroot", existing.ProfilePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
                if (System.IO.File.Exists(fullPath))
                {
                    try { System.IO.File.Delete(fullPath); } catch { /* ignore cleanup errors */ }
                }
            }

            return NoContent();
        }

        // POST: api/Candidate/notifyhr/5
        [HttpPost("notifyhr/{candidateId:int}")]
        public async Task<IActionResult> NotifyHR(int candidateId)
        {
            var candidate = await _unitOfWork.Candidate.GetByIdAsync(candidateId);
            if (candidate == null) return NotFound();

            candidate.OnboardingRequested = true;
            _unitOfWork.Candidate.Update(candidate);
            _unitOfWork.Save();

            // TODO: enqueue notification / send email
            return Ok(new { message = $"HR notified to onboard {candidate.CadidateName}" });
        }

        // GET: api/Candidate/5/profile
        [HttpGet("{id:int}/profile")]
        public async Task<IActionResult> DownloadProfile(int id)
        {
            var candidate = await _unitOfWork.Candidate.GetByIdAsync(id);
            if (candidate == null || string.IsNullOrEmpty(candidate.ProfilePath)) return NotFound();

            var fullPath = Path.Combine(_env.WebRootPath ?? "wwwroot", candidate.ProfilePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));
            if (!System.IO.File.Exists(fullPath)) return NotFound();

            var fileBytes = await System.IO.File.ReadAllBytesAsync(fullPath);
            var fileName = Path.GetFileName(fullPath);
            return File(fileBytes, "application/octet-stream", fileName);
        }
    }
}