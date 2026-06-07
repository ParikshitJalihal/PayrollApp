using HCM.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.PayrollClient.Interfaces
{
    public interface IAttendanceClient
    {
        Task<IEnumerable<TimeSheetEntry>> GetAllAsync();
        Task<TimeSheetEntry?> GetByIdAsync(int? id);
        Task UpsertEntry(TimeSheetEntry attendance);
        Task DeleteAsync(int id);
        Task<TaskItem> GetTaskItem(int? id);
        Task<IEnumerable<TaskItem>> GetAllTaskItems();
    }
}
