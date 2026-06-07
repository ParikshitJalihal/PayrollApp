using HCM.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Models.ViewModels
{
    public class EmployeeVM
    {
        public Employee Employee { get; set; } = new Employee();
        [ValidateNever]
        public IEnumerable<SelectListItem> JobList { get; set; }
        [ValidateNever]
        public List<SelectListItem> ReportingManagerList { get; set; }
        [ValidateNever]
        public List<SelectListItem> DepartmentList { get; set; }
        [ValidateNever]
        public List<SelectListItem> GenderList { get; set; }
        [ValidateNever]
        public List<SelectListItem> DesignationList { get; set; }

        public string? DepartmentName { get; set; }
        public string? DesignationName { get; set; }
        public string? ReportingManagerName { get; set; }

        public string? JobName { get; set; }
        public string? EmployeeCode { get; set; }
        public string? EmployeeName { get; set; }
        public string? EmployeePhone { get; set; }
        public string? EmployeeEmail { get; set; }
        public DateTime? JoiningDate { get; set; }
        public List<PayComponentModel> PayComponents { get; set; }

    }
}
