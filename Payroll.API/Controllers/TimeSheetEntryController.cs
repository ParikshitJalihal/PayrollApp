using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HCMWebApp.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class TimeSheetEntryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TimeSheetEntryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<TimeSheetEntry> timeSheetEntries = _unitOfWork.TimeSheet.GetAll()
                .ToList();
            timeSheetEntries.ForEach(entry =>
            {
                entry.Task = _unitOfWork.TaskRepo.Get(t => t.TaskId == entry.TaskId);
            });

            return View(timeSheetEntries);
        }

        public IActionResult Create()
        {
            var timeSheetEntryView = new TimeSheetEntryView
            {
                TaskList = _unitOfWork.TaskRepo.GetAll()
                    .Select(t => new SelectListItem { Value = t.TaskId.ToString(), Text = t.Name })
                    .ToList()
            };

            return View("Create", timeSheetEntryView);
        }


        [HttpPost]
        public IActionResult Create(TimeSheetEntryView? timeSheetEntry)
        {

            if (ModelState.IsValid)
            {
                timeSheetEntry.TimeSheetEntry = new TimeSheetEntry
                {
                    EntryDate = timeSheetEntry.EntryDate,
                    LogHours = (decimal)timeSheetEntry.LogHours.Hour + ((decimal)timeSheetEntry.LogHours.Minute / 60),
                    TaskId = int.Parse(timeSheetEntry.TaskName), // Assuming TaskName holds the TaskId as string
                    SubTask = timeSheetEntry.SubTask,
                    Description = timeSheetEntry.Description,
                    CreatedDate = DateTime.UtcNow
                };
                _unitOfWork.TimeSheet.Add(timeSheetEntry.TimeSheetEntry);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(timeSheetEntry);


        }
    }
}
