using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Models.Models
{
    public class RequisiteDetails
    {
        [Key]
        public int RequisiteDetailsId { get; set; }
        public int? ReqId { get; set; }
        [ForeignKey("ReqId")]
        public Requesites? Requesites { get; set; }  
        public string? RequisiteName { get; set; }
        public string? RequisiteValue { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
