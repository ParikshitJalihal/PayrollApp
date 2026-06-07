using HCM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Models.ViewModels
{
    public class PayComponentModel
    {
        public int PayComponentId { get; set; }
        public string? ComponentName { get; set; }
        public string? ComponentType { get; set; }
        public string? PayCode { get; set; }

        public decimal MonthlyAmount { get; set; }
        public decimal AnnualAmount { get; set; }

        public decimal Amount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public int CompanyId { get; set; }
       
    }
}
