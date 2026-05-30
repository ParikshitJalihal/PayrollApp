using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MastersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public MastersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Masters
        // Returns all RequisiteDetails including their Requesites (type)
        [HttpGet]
        public ActionResult<IEnumerable<RequisiteDetails>> GetAll()
        {
            var requisites = _unitOfWork.RequisiteDetails.GetAll(includeProperties: "Requesites");
            return Ok(requisites);
        }

        // GET: api/Masters/5
        [HttpGet("{id:int}")]
        public ActionResult<RequisiteDetails> GetById(int id)
        {
            var item = _unitOfWork.RequisiteDetails.Get(r => r.RequisiteDetailsId == id, includeProperties: "Requesites");
            if (item == null) return NotFound();
            return Ok(item);
        }

        // GET: api/Masters/requesites
        // Returns the master types (Requesites)
        [HttpGet("requesites")]
        public ActionResult<IEnumerable<Requesites>> GetMasterTypes()
        {
            var types = _unitOfWork.Requesites.GetAll().ToList();
            return Ok(types);
        }

        // POST: api/Masters
        // Create a new RequisiteDetails entry
        [HttpPost]
        public ActionResult<RequisiteDetails> Create([FromBody] RequisiteDetails dto)
        {
            if (dto == null) return BadRequest();
            dto.CreatedDate = dto.CreatedDate == default ? DateTime.UtcNow : dto.CreatedDate;

            _unitOfWork.RequisiteDetails.Add(dto);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetById), new { id = dto.RequisiteDetailsId }, dto);
        }

        // POST: api/Masters/from-form
        // Compatibility endpoint for form-posts that send ReqId and MasterName in body (form-style)
        [HttpPost("from-form")]
        public ActionResult CreateFromForm([FromForm] int ReqId, [FromForm] string MasterName)
        {
            if (string.IsNullOrWhiteSpace(MasterName)) return BadRequest("MasterName is required.");

            var masterEntity = new RequisiteDetails
            {
                ReqId = ReqId,
                RequisiteName = MasterName,
                RequisiteValue = MasterName,
                CreatedDate = DateTime.UtcNow
            };

            _unitOfWork.RequisiteDetails.Add(masterEntity);
            _unitOfWork.Save();

            return CreatedAtAction(nameof(GetById), new { id = masterEntity.RequisiteDetailsId }, masterEntity);
        }

        // PUT: api/Masters/5
        [HttpPut("{id:int}")]
        public ActionResult Update(int id, [FromBody] RequisiteDetails dto)
        {
            if (dto == null || id != dto.RequisiteDetailsId) return BadRequest();

            var existing = _unitOfWork.RequisiteDetails.Get(r => r.RequisiteDetailsId == id);
            if (existing == null) return NotFound();

            // map updatable fields
            existing.ReqId = dto.ReqId;
            existing.RequisiteName = dto.RequisiteName;
            existing.RequisiteValue = dto.RequisiteValue;
            //existing.dat = DateTime.UtcNow;

            _unitOfWork.RequisiteDetails.Update(existing);
            _unitOfWork.Save();

            return NoContent();
        }

        // DELETE: api/Masters/5
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var existing = _unitOfWork.RequisiteDetails.Get(r => r.RequisiteDetailsId == id);
            if (existing == null) return NotFound();

            _unitOfWork.RequisiteDetails.Delete(existing);
            _unitOfWork.Save();

            return NoContent();
        }
    }
}