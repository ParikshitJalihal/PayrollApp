using HCM.DataAccess.Data;
using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace HCMWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class JobsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public JobsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var jobs = _unitOfWork.Jobs.GetAll();
            return View(jobs);

        }

        public IActionResult Upsert(int? id)
        {
            Jobs objJobs = new Jobs();
            if (id == null || id == 0)
            {
                // Create
                return View(objJobs);
            }
            else
            {
                // Update
                objJobs = _unitOfWork.Jobs.Get(u => u.JobId == id);
                if (objJobs == null)
                {
                    return NotFound();
                }
                return View(objJobs);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Jobs jobs)
        {
            if (ModelState.IsValid)
            {
                if (jobs.JobId == 0)
                {
                    _unitOfWork.Jobs.Add(jobs);
                    // Create
                }
                else
                {
                    // Update
                    _unitOfWork.Jobs.Update(jobs);
                }
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(jobs);
        }

        public IActionResult Delete(int id)
        {
            Jobs objJobs = new Jobs();
            if (ModelState.IsValid)
            {
                
                if (id == null || id == 0)
                {
                    // Create
                    return View(objJobs);
                }
                else
                {
                    // Update
                    objJobs = _unitOfWork.Jobs.Get(u => u.JobId == id);
                    if (objJobs == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        _unitOfWork.Jobs.Delete(objJobs);
                    }
                }
            }
            return RedirectToAction("Index");

        }

        
    }
}
