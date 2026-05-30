using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Services.PayrollClient.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HCMWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MastersController : Controller
    {
        private readonly IRequisiteClient _reqClient;
        public MastersController(IRequisiteClient reqClient)
        {
            _reqClient = reqClient;
        }
        public IActionResult Index()
        {
            IEnumerable<RequisiteDetails> requisistes = _reqClient.GetAllAsync().Result;
            return View(requisistes);
        }



        [HttpGet]
        public IActionResult CreateMaster()
        {
            var lstRequests = _reqClient.GetRequesites().Result;
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
                var lstRequests = _reqClient.GetRequesites().Result;
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
            if (masterEntity.RequisiteDetailsId == 0)
            {
                _reqClient.AddAsync(masterEntity);
            }
            else
                _reqClient.UpdateAsync(masterEntity);

            TempData["success"] = "Master created successfully!";
            return RedirectToAction("Index"); // or wherever you list masters
        }

    }
}
