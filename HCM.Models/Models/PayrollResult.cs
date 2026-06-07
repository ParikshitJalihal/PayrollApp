using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Models.Models
{
    public class PayrollResult
    {
        [Key]
        public int PayrollResultId { get; set; }
        public int EmployeeId { get; set; }
        public decimal GrossPay { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal NetPay { get; set; }
        public DateTime PayPeriodStart { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public DateTime ProcessedDate { get; set; } = DateTime.Now;

        public Employee Employee { get; set; }
        public ICollection<EmployeePay> EmployeePays { get; set; }
    }

}
