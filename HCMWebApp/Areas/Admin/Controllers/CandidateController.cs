using HCM.DataAccess.Data;
using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HCMWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CandidateController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CandidateController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var candidates = _unitOfWork.Candidate.GetAll(includeProperties: "Jobs");
            return View(candidates);
        }
        public IActionResult ApplyForJob()
        {
            Candidate objCandidate = new Candidate();
            return View(objCandidate);
        }

        public IActionResult Details(int id)
        {
            var candidate = _unitOfWork.Candidate.Get(u => u.CandidateId == id, includeProperties: "Jobs");
            if (candidate == null)
            {
                return NotFound();
            }
            CandidateVM objCandidate = new CandidateVM()
            {
                Candidate = candidate,
                JobList = _unitOfWork.Jobs.GetAll().Select(u => new SelectListItem
                {
                    Text = u.JobName,
                    Value = u.JobId.ToString()
                }).ToList()
            };
            objCandidate.Candidate = candidate;
            return View(objCandidate);
        }

        public IActionResult UpSert(int id)
        {
            IEnumerable<SelectListItem> selectListItems = _unitOfWork.Jobs.GetAll().Select(u => new SelectListItem
            {
                Text = u.JobName,
                Value = u.JobId.ToString()
            });
            CandidateVM objCandidateVM = new CandidateVM()
            {
                JobList = selectListItems.ToList()
            };
            Candidate objCandidate = new Candidate();
            if (id == 0)
            {
                objCandidateVM.Candidate = new Candidate();
                return View(objCandidateVM);
            }
            else
            {
                // Update
                objCandidate = _unitOfWork.Candidate.Get(u => u.CandidateId == id);
                if (objCandidate == null)
                {
                    return NotFound();
                }
                objCandidateVM.Candidate = objCandidate;
                return View(objCandidateVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(CandidateVM objCandidateVM, IFormFile file)
        {

            if (ModelState.IsValid)
            {

                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var savePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads/profiles/profile-0", fileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                         file.CopyToAsync(stream);
                    }

                    // Store relative web path, not physical path
                    objCandidateVM.Candidate.ProfilePath = "/uploads/profiles/profile-0/" + fileName;

                }
                if (objCandidateVM.Candidate.CandidateId == 0)
                {
                    // Create
                    _unitOfWork.Candidate.Add(objCandidateVM.Candidate);
                }
                else
                {
                    // Update
                    _unitOfWork.Candidate.Update(objCandidateVM.Candidate);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(objCandidateVM);
        }

        public IActionResult DownloadFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return NotFound();

            // Map relative path to physical path
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath.TrimStart('/'));

            if (!System.IO.File.Exists(fullPath))
                return NotFound();

            var contentType = "application/octet-stream";
            var fileName = Path.GetFileName(fullPath);

            var fileBytes = System.IO.File.ReadAllBytes(fullPath);
            return File(fileBytes, contentType, fileName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NotifyHR(int candidateId)
        {
            // Find candidate by ID
            var candidate = _unitOfWork.Candidate.Get(i=>i.CandidateId == candidateId);
            if (candidate == null)
            {
                return NotFound();
            }

            // Flag candidate for onboarding
            candidate.OnboardingRequested = true;

            _unitOfWork.Candidate.Update(candidate);
             _unitOfWork.Save();
            // Example: feedback message to UI
            TempData["Message"] = $"HR notified to onboard {candidate.CadidateName}.";

            // TODO: send email/notification to HR here
            // e.g. using SMTP, SendGrid, or any notification service

            return RedirectToAction(nameof(Index));
        }


    }
}
