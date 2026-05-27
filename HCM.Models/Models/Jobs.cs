using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Models.Models
{
    public class Jobs
    {
        [Key]
        public int JobId { get; set; }
        public string JobName { get; set; }
        public bool JobStatus { get; set; } // Open or Closed
        public string JobStatusDescription { get; set; }
    }
}
