using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Models.ViewModels;
using HCM.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void UpSertEmployee(EmployeeVM employeeVM)
        {
            if (employeeVM.Employee.EmployeeId == 0)
            {
                if (employeeVM.Employee.CandidateId != null && employeeVM.Employee.CandidateId > 0)
                {
                    var candidate = _unitOfWork.Candidate.Get(i => i.CandidateId == employeeVM.Employee.CandidateId);
                    if (candidate != null)
                    {
                        candidate.OnboardingRequested = false; // Mark onboarding as completed
                        _unitOfWork.Candidate.Update(candidate);
                    }
                }
                // Create

                _unitOfWork.Employee.Add(employeeVM.Employee);
            }
            else
            {
                // Update
                _unitOfWork.Employee.Update(employeeVM.Employee);
            }
            _unitOfWork.Save();
        }

        public List<EmployeeVM>  ListEmployee()
        {
            List<EmployeeVM> lstEmployeeVM = new List<EmployeeVM>();
            var employees = _unitOfWork.Employee.GetAll(includeProperties: "Jobs");
            var requisiteDetails = _unitOfWork.RequisiteDetails.GetAll(includeProperties: "Requesites").ToList();


            foreach (var emp in employees)
            {
                RequisiteDetails departmentDetail = requisiteDetails.FirstOrDefault(rd => rd.RequisiteDetailsId == emp.DepartmentId);
                if (departmentDetail == null)
                    departmentDetail = new RequisiteDetails();
                EmployeeVM employeeVM = new EmployeeVM
                {
                    EmployeeName = emp.EmployeeName,
                    EmployeeCode = emp.EmployeeCode,
                    EmployeePhone = emp.Phone,
                    EmployeeEmail = emp.Email,
                    JoiningDate = emp.JoiningDate,
                    Employee = emp,
                    JobName = emp.Jobs != null ? emp.Jobs.JobName : "N/A",
                    DepartmentName = departmentDetail?.Requesites?.ReqName == "Department" ? departmentDetail.RequisiteValue : "N/A",
                };
                lstEmployeeVM.Add(employeeVM);
            }

            return lstEmployeeVM;
        }

        
    }
}
