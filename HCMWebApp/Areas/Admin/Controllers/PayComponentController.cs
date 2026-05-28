using HCM.Models.Models;
using HCM.Models.ViewModels;
using HCM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HCMWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PayComponentController : Controller
    {

        private readonly IComponentService _componentService;
        public PayComponentController(IComponentService componentService)
        {
            _componentService = componentService;
        }
        public IActionResult Index()
        {
            var components = _componentService.ListPayComponent();
            return View(components);
        }

        public IActionResult Create()
        {
            ComponentVM componentVM = new ComponentVM
            {
                PayComponent = new PayComponent()
            };

            componentVM.ComponentMapToList = _componentService.GetComponentMapToList();
            return View(componentVM);
        }

        [HttpPost]
        public IActionResult Create(ComponentVM componentVM)
        {
            if (ModelState.IsValid)
            {
                _componentService.UpSertPayComponent(componentVM.PayComponent);
                return RedirectToAction(nameof(Index));
            }
            componentVM.ComponentMapToList = _componentService.GetComponentMapToList();
            return View(componentVM);
        }




    }
}
