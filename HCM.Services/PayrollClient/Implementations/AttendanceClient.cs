using HCM.Models.Models;
using HCM.Services.PayrollClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.PayrollClient.Implementations
{
    public class AttendanceClient : IAttendanceClient
    {
        private readonly HttpClient _http;
        public AttendanceClient(HttpClient http) => _http = http;
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<TimeSheetEntry>> GetAllAsync()
        {
            var res = _http.GetFromJsonAsync<IEnumerable<TimeSheetEntry>>("api/TimeSheetEntry");
            return res ?? Task.FromResult(Enumerable.Empty<TimeSheetEntry>());
        }

        public Task<IEnumerable<TaskItem>> GetAllTaskItems()
        {
           var res = _http.GetFromJsonAsync<IEnumerable<TaskItem>>("api/TaskItem");
            return res ?? Task.FromResult(Enumerable.Empty<TaskItem>());
        }

        public Task<TimeSheetEntry?> GetByIdAsync(int? id)
        {
            var resp = _http.GetAsync($"api/TimeSheetEntry/{id}");
            if (resp.Result.StatusCode == System.Net.HttpStatusCode.NotFound)
                return Task.FromResult<TimeSheetEntry?>(null);
            else
               return resp.Result.Content.ReadFromJsonAsync<TimeSheetEntry>();
        }

        public Task<TaskItem> GetTaskItem(int? id)
        {
            var resp = _http.GetAsync($"api/TaskItem/{id}");
            return resp.ContinueWith(t =>
            {
                return t.Result.StatusCode == System.Net.HttpStatusCode.NotFound ? null : 
                t.Result.Content.ReadFromJsonAsync<TaskItem>().Result;
            });
        }

        public Task UpsertEntry(Models.Models.TimeSheetEntry attendance)
        {
            var resp = _http.PutAsJsonAsync($"api/TimeSheetEntry/{attendance.EntryId}", attendance);
            return resp.ContinueWith(t =>
            {
                if (t.Result.IsSuccessStatusCode) return;
                else throw new Exception($"Failed to upsert attendance entry with ID {attendance.EntryId}");
            });
        }

        
    }
}
