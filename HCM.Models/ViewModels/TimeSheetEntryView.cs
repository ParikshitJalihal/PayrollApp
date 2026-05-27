
using HCM.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace HCM.Models.ViewModels
{
    public class TimeSheetEntryView
    {
        public TimeSheetEntry? TimeSheetEntry { get; set; }
        public DateTime EntryDate { get; set; } = DateTime.Now.Date;
        public TimeOnly LogHours { get; set; }
        public string? TaskName { get; set; }
        public string? SubTask { get; set; }
        public string? Description { get; set; }

        [ValidateNever]
        public List<SelectListItem> TaskList { get; set; }

        // Audit fields
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
