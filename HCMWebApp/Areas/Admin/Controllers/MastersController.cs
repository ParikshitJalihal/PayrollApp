using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HCMWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MastersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MastersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            IEnumerable<RequisiteDetails> requisistes = _unitOfWork.RequisiteDetails.GetAll(includeProperties : "Requesites");
            return View(requisistes);
        }

        

        [HttpGet]
        public IActionResult CreateMaster()
        {
            var lstRequests = _unitOfWork.Requesites.GetAll().ToList();
            var selectListItems = lstRequests.Select(r => new SelectListItem
            {
                Text = r.ReqName,
                Value = r.ReqId.ToString()
            }).ToList();

            return View(selectListItems);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMaster(int ReqId, string MasterName)
        {
            if (string.IsNullOrWhiteSpace(MasterName))
            {
                ModelState.AddModelError("MasterName", "Master name is required.");
            }

            if (!ModelState.IsValid)
            {
                // reload dropdown if validation fails
                var lstRequests = _unitOfWork.Requesites.GetAll().ToList();
                var selectListItems = lstRequests.Select(r => new SelectListItem
                {
                    Text = r.ReqName,
                    Value = r.ReqId.ToString()
                }).ToList();

                return View(selectListItems);
            }

            // Example entity for saving master
            var masterEntity = new RequisiteDetails
            {
                ReqId = ReqId,
                RequisiteName = MasterName,
                RequisiteValue = MasterName,
                CreatedDate = DateTime.Now
            };

            _unitOfWork.RequisiteDetails.Add(masterEntity);
            _unitOfWork.Save();

            TempData["success"] = "Master created successfully!";
            return RedirectToAction("Index"); // or wherever you list masters
        }

    }
}
