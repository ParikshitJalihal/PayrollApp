using HCM.Models.Models;
using HCM.Services.PayrollClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace HCM.Services.PayrollClient.Implementations
{
    public class JobsClient : IJobsClient
    {
        private readonly HttpClient _http;
        public JobsClient(HttpClient http) => _http = http;
        public Task AddJob(Jobs job)
        {
            var res = _http.PostAsJsonAsync("api/Jobs", job);
            return res.ContinueWith(t =>
            {
                if (t.IsFaulted) throw t.Exception ?? new Exception("Unknown error");
                var resp = t.Result;
                resp.EnsureSuccessStatusCode();
            });
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Jobs>> GetAllAsync()
        {
            var res = await _http.GetFromJsonAsync<IEnumerable<Jobs>>("api/Jobs");
            return res ?? Enumerable.Empty<Jobs>();
        }

        public Task<Jobs?> GetByIdAsync(int id)
        {
            var res = _http.GetAsync($"api/Jobs/{id}");
            return res.ContinueWith(t =>
            {
                if (t.IsFaulted) throw t.Exception ?? new Exception("Unknown error");
                var resp = t.Result;
                if (resp.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                resp.EnsureSuccessStatusCode();
                return resp.Content.ReadFromJsonAsync<Jobs>().Result;
            });
        }

        public Task UpdateAsync(Jobs job)
        {
           var res = _http.PutAsJsonAsync($"api/Jobs/{job.JobId}", job);
            return res.ContinueWith(t =>
            {
                if (t.IsFaulted) throw t.Exception ?? new Exception("Unknown error");
                var resp = t.Result;
                resp.EnsureSuccessStatusCode();
            });
        }
    }
}
