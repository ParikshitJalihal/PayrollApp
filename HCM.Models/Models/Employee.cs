using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Models.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Display(Name ="Name of Employee : ")]
        public string? EmployeeName { get; set; }
        [Display(Name ="Age of Employee (as of today)")]
        public int EmployeeAge { get; set; }
        public bool EmployeeStatus { get; set; }
        [Display(Name ="Date of Birth :")]
        public DateTime EmployeeDob { get; set; }
        [Display(Name ="Joining Date:")]
        public DateTime JoiningDate { get; set; }
        [Display(Name ="Annual Compensation")]
        public double EmployeeCTC { get; set; }
        [Display(Name ="Annaul Base Pay")]
        public double EmployeeBasic { get; set; }
        [Display(Name ="Reports To:")]
        public int EmployeeManager { get; set; }
        [Display(Name ="Is He Reporting Manager ?")]
        public bool isManager { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
        public string? Department { get; set; }
        [Display(Name = "Job Type")]
        public int? JobId { get; set; }
        [Display(Name ="Employee Code :")]
        public string? EmployeeCode { get; set; }
        public virtual Jobs? Jobs { get; set; }

        public int? CandidateId { get; set; }
        [ForeignKey("CandidateId")]
        public Candidate? Candidate { get; set; }
        [Display(Name ="Department :")]
        public int? DepartmentId { get; set; }
        [Display(Name ="Designation :")]

        public int? DesignationId { get; set; }

        public ICollection<EmployeePay> EmployeePayments { get; set; } = new List<EmployeePay>();
    }
}
