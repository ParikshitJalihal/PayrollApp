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
    public class CandidateVM
    {
        public Candidate? Candidate { get; set; }
        [ValidateNever]
        public List<SelectListItem> JobList { get; set; }
    }
}
