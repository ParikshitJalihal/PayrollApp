using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Models.ViewModels;
using HCM.Services.PayrollClient.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HCMWebApp.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class TimeSheetEntryController : Controller
    {
        private readonly IAttendanceClient _attendanceClient;
        public TimeSheetEntryController(IAttendanceClient attendanceClient)
        {
            _attendanceClient = attendanceClient;
        }
        public IActionResult Index()
        {
            List<TimeSheetEntry> timeSheetEntries = _attendanceClient.GetAllAsync().Result
                .ToList();
            timeSheetEntries.ForEach(entry =>
            {
                entry.Task = _attendanceClient.GetTaskItem(entry.TaskId).Result;
            });

            return View(timeSheetEntries);
        }

        public IActionResult Create()
        {
            var timeSheetEntryView = new TimeSheetEntryView
            {
                TaskList = _attendanceClient.GetAllTaskItems().Result
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
                _attendanceClient.UpsertEntry(timeSheetEntry.TimeSheetEntry).Wait();
                return RedirectToAction(nameof(Index));
            }
            return View(timeSheetEntry);


        }
    }
}
