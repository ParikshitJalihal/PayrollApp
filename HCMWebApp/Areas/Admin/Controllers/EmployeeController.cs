using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Models.ViewModels;
using HCM.Services.Interfaces;
using HCM.Services.PayrollClient.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HCMWebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeClient _employeeClient;
        private readonly IJobsClient _jobsClient;
        private readonly ICandidateClient _candidateClient;
        private readonly IRequisiteClient _requisiteClient;
        public EmployeeController(IEmployeeClient employeeClient, IJobsClient jobsClient, ICandidateClient candidateClient, IRequisiteClient requisiteClient)
        {

            _employeeClient = employeeClient;
            _jobsClient = jobsClient;
            _candidateClient = candidateClient;
            _requisiteClient = requisiteClient;
        }

        public IActionResult Index()
        {
            var awaitingCandidates = _candidateClient.GetAllAsync().Result.Where(c => c.OnboardingRequested).ToList();
            return View(awaitingCandidates);
        }

        public IActionResult Create()
        {
            EmployeeVM employeeVM = new()
            {
                JobList = _jobsClient.GetAllAsync().Result.Select(jobList => new SelectListItem
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
            var candidate = _candidateClient.GetByIdAsync(candidateId).Result;
            if (candidate == null || !candidate.OnboardingRequested)
            {
                return NotFound();
            }

            var jobList = _jobsClient.GetAllAsync().Result.ToList();

            EmployeeVM employeeVM = new()
            {
                JobList = _jobsClient.GetAllAsync().Result.Select(jobList => new SelectListItem
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
                employeeVM.JobList = new SelectList(_jobsClient.GetAllAsync().Result, "JobId", "JobTitle");
                return View(employeeVM);
            }

            _employeeClient.UpdateAsync(employeeVM.Employee);


            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult IndexEmployee()
        {
            var employees = _employeeClient.GetAllAsync().Result;
            List<EmployeeVM> employeeVMs = new List<EmployeeVM>();
            foreach( Employee emp in employees)
            {
                EmployeeVM employeeVM = new EmployeeVM()
                {
                    Employee = emp
                };
                employeeVMs.Add(employeeVM);
            }

            return View(employeeVMs);

        }

        public IActionResult UpSert(int? id)
        {
            try
            {
                EmployeeVM employeeVM = new()
                {
                    JobList = _jobsClient.GetAllAsync().Result.Select(jobList => new SelectListItem
                    {
                        Value = jobList.JobId.ToString(),
                        Text = jobList.JobName
                    }),

                    DepartmentList = _requisiteClient.GetAllAsync().Result.
                    ToList().Where(e => e.Requesites?.ReqName == "Department").
                    Select(dept => new SelectListItem
                    {
                        Value = dept.RequisiteDetailsId.ToString(),
                        Text = dept.RequisiteValue
                    }).ToList(),
                    ReportingManagerList = _employeeClient.GetAllAsync().Result.
                    ToList().
                    Where(e => e.isManager == true).
                    Select(e => e.EmployeeName).Distinct().
                    Select(manager => new SelectListItem
                    {
                        Value = manager,
                        Text = manager
                    }).ToList(),
                    DesignationList = _requisiteClient.GetAllAsync().Result.ToList().Where(e => e.Requesites?.ReqName == "Designation").
                    Select(dept => new SelectListItem
                    {
                        Value = dept.RequisiteDetailsId.ToString(),
                        Text = dept.RequisiteValue
                    }).ToList(),

                    GenderList = _requisiteClient.GetAllAsync().Result.
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
                    employeeVM.Employee = _employeeClient.GetByIdAsync(id).Result;
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
            var employee = _employeeClient.GetByIdAsync(id).Result;
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            var employee = _employeeClient.GetByIdAsync(id).Result;
            if (employee == null)
            {
                return NotFound();
            }
            //_unitOfWork.Employee.Delete(employee);
            //_unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}