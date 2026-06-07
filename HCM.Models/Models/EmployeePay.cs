using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Models.Models
{
    [Table("EmployeePayment")]
    public class EmployeePay
    {
        public int EmployeePayId { get; set; }
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public  Employee? Employee { get; set; }
        public decimal MonthlyAmount { get; set; }
        public decimal AnnualAmount { get; set; }

        public DateTime EffectiveDate { get; set; }
        public DateTime? EffectiveEndDate { get; set; }

        public DateTime PayPeriodStart { get; set; }
        public DateTime PayPeriodEnd { get; set; }

        public DateTime  CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }  
        public string CreatedBy { get; set; } = string.Empty;
        public string ModifiedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }
        public int CompanyId { get; set; }
        public int BranchId { get; set; }

        public int PayComponentId { get; set; }
        [ValidateNever]
        public PayComponent PayComponent { get; set; }
        public DateTime PayDate { get; set; }
        
    }
}
