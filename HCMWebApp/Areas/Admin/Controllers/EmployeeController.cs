using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Models.ViewModels;
using HCM.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HCMWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IUnitOfWork unitOfWork,IEmployeeService employeeservice)
        {
            _unitOfWork = unitOfWork;
            _employeeService = employeeservice;
        }

        public IActionResult Index()
        {
            var awaitingCandidates = _unitOfWork.Candidate.GetAll(i => i.OnboardingRequested, includeProperties: "Jobs");
            return View(awaitingCandidates);
        }

        public IActionResult Create()
        {
            EmployeeVM employeeVM = new()
            {
                JobList = _unitOfWork.Jobs.GetAll().Select(jobList => new SelectListItem
                {
                    Value = jobList.JobId.ToString(),
                    Text = jobList.JobName
                }),
                Employee = new HCM.Models.Models.Employee()
            };
            return View(employeeVM);
        }

        public IActionResult EmployeeUpsert(int candidateId)
        {
            var candidate = _unitOfWork.Candidate.Get(i => i.CandidateId == candidateId);
            if (candidate == null || !candidate.OnboardingRequested)
            {
                return NotFound();
            }

            var jobList = _unitOfWork.Jobs.GetAll().ToList();

            EmployeeVM employeeVM = new()
            {
                JobList = _unitOfWork.Jobs.GetAll().Select(jobList => new SelectListItem
                {
                    Value = jobList.JobId.ToString(),
                    Text = jobList.JobName
                }),
                Employee = new HCM.Models.Models.Employee()
            };
            var objEmployee = new HCM.Models.Models.Employee()
            {
                EmployeeName = candidate.CadidateName,
                Email = candidate.CandidateEmail,
                Phone = candidate.CandidatePhone,
                Gender = candidate.CandidateGender,
                Department = candidate.JobDescription,
                JobId = candidate.JobId,
                JoiningDate = DateTime.Now.Date,
                EmployeeCode = "AUTO-" + candidate.CandidateId,
                CandidateId = candidate.CandidateId

            };
            employeeVM.Employee = objEmployee;

            // Return the vm we just constructed (previous code returned a different new instance).
            return View(employeeVM);
        }



        [HttpPost]
        public IActionResult EmployeeUpsert(EmployeeVM employeeVM)
        {
            if (employeeVM == null || employeeVM.Employee == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // Re-populate JobList when returning to the view after validation errors
                employeeVM.JobList = new SelectList(_unitOfWork.Jobs.GetAll(), "JobId", "JobTitle");
                return View(employeeVM);
            }

            _employeeService.UpSertEmployee(employeeVM);


            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult IndexEmployee()
        {
            var employees = _employeeService.ListEmployee();

            return View(employees);

        }

        public IActionResult UpSert(int? id)
        {
            try
            {
                EmployeeVM employeeVM = new()
                {
                    JobList = _unitOfWork.Jobs.GetAll().Select(jobList => new SelectListItem
                    {
                        Value = jobList.JobId.ToString(),
                        Text = jobList.JobName
                    }),

                    DepartmentList = _unitOfWork.RequisiteDetails.GetAll(includeProperties: "Requesites").
                    ToList().Where(e => e.Requesites?.ReqName == "Department").
                    Select(dept => new SelectListItem
                    {
                        Value = dept.RequisiteDetailsId.ToString(),
                        Text = dept.RequisiteValue
                    }).ToList(),
                    ReportingManagerList = _unitOfWork.Employee.GetAll().
                    ToList().
                    Where(e => e.isManager == true).
                    Select(e => e.EmployeeName).Distinct().
                    Select(manager => new SelectListItem
                    {
                        Value = manager,
                        Text = manager
                    }).ToList(),
                    DesignationList = _unitOfWork.RequisiteDetails.GetAll(includeProperties: "Requesites").
                    ToList().Where(e => e.Requesites?.ReqName == "Designation").
                    Select(dept => new SelectListItem
                    {
                        Value = dept.RequisiteDetailsId.ToString(),
                        Text = dept.RequisiteValue
                    }).ToList(),

                    GenderList = _unitOfWork.RequisiteDetails.GetAll(includeProperties: "Requesites").
                    ToList().Where(e => e.Requesites?.ReqName == "Gender").
                    Select(dept => new SelectListItem
                    {
                        Value = dept.RequisiteDetailsId.ToString(),
                        Text = dept.RequisiteValue
                    }).ToList(),

                    Employee = new HCM.Models.Models.Employee()
                };
                if (id == null || id == 0)
                {
                    // Create
                    return View(employeeVM);
                }
                else
                {
                    // Update
                    employeeVM.Employee = _unitOfWork.Employee.Get(u => u.EmployeeId == id);
                    if (employeeVM.Employee == null)
                    {
                        return NotFound();
                    }
                    return View(employeeVM);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"An error occurred: {ex.Message}");
                // Optionally, you can return an error view or a user-friendly message
                return View("Error", new { message = "An error occurred while processing your request." });
            }
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var employee = _unitOfWork.Employee.Get(u => u.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var employee = _unitOfWork.Employee.Get(u => u.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            _unitOfWork.Employee.Delete(employee);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}