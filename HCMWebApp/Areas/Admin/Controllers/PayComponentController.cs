using HCM.Models.Models;
using HCM.Models.ViewModels;
using HCM.Services.Interfaces;
using HCM.Services.PayrollClient.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HCMWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PayComponentController : Controller
    {

        private readonly IPayrollClient _payrollClient;
        public PayComponentController(IPayrollClient payrollClient)
        {
            _payrollClient = payrollClient;
           
        }
        public IActionResult Index()
        {
            var components = _payrollClient.GetAllAsync().Result;
            return View(components);
        }

        public IActionResult Create()
        {
            ComponentVM componentVM = new ComponentVM
            {
                PayComponent = new PayComponent()
            };

            componentVM.ComponentMapToList = GetComponentMapToList();
            return View(componentVM);
        }

        [HttpPost]
        public IActionResult Create(ComponentVM componentVM)
        {
            if (ModelState.IsValid)
            {
                var result = _payrollClient.UpsertAsync(componentVM.PayComponent);
                return RedirectToAction(nameof(Index));
            }
            componentVM.ComponentMapToList = GetComponentMapToList();
            return View(componentVM);
        }

        private List<SelectListItem> GetComponentMapToList()
        {
            return new List<SelectListItem>()
            {
                new SelectListItem { Value = "1", Text = "Provident Fund" },
                new SelectListItem { Value = "2", Text = "Employer PF" },
                new SelectListItem { Value = "3", Text = "Reimbursement" },
                new SelectListItem { Value = "4", Text = "Other" }
            };
        }




    }
}
