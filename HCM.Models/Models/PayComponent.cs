using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Models.Models
{
    public class PayComponent
    {
        [Key]
        public int PayComponentId { get; set; }
        [Required]
        public string? ComponentName { get; set; }
        [Required]
        public string? ComponentType { get; set; } // Earning or Deduction
        [Required]
        public string? PayFormula { get; set; }
        public string? PayTypeCode { get; set; }
        public bool IsStatutory { get; set; }
        public bool IsActive { get; set; }
        public bool IsPayable { get; set; }
        public bool IsTaxable { get; set; }
        public bool hasPayComponentGroup { get; set; } // For grouping pay components
        public string? PayMapTo { get; set; } // For mapping to payroll systems like PF, ESI etc.
        public string? Description { get; set; }
        public string? MappingCode { get; set; } // For integration with external systems
        public string? MappingName { get; set; } // For integration with external systems
        public string? MappingType { get; set; } // For integration with external systems
        public string? MappingTypeCode { get; set; } // For integration with external systems

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;


    }
}
