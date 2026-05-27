using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Models.Models
{
    public class Requesites
    {
        [Key]
        public int ReqId { get; set; }
        public string? ReqName { get; set; }
        public string? ReqDescription { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
