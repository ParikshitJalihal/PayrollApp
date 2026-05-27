using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace HCMWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TaskItemController : Controller
    {
        private readonly IUnitOfWork _unitOfwork;
        public  TaskItemController(IUnitOfWork unitOfwork)
        {
            _unitOfwork = unitOfwork;
        }
        public IActionResult Index()
        {
            List<TaskItem> lstTAskItem = _unitOfwork.TaskRepo.GetAll().ToList();
            return View(lstTAskItem);
        }

        public IActionResult Create()
        {
            TaskItem objTaskItem = new TaskItem();
            return View(objTaskItem);
        }
        [HttpPost]
        public IActionResult Create(TaskItem objTaskItem)
        {
            _unitOfwork.TaskRepo.Add(objTaskItem);
            _unitOfwork.Save();
            return View();
        }
    }
}
