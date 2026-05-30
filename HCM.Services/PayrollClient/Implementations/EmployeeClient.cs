using HCM.Models.Models;
using HCM.Services.PayrollClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace HCM.Services.PayrollClient.Implementations
{
    public class EmployeeClient : IEmployeeClient
    {
        private readonly HttpClient _http;
        public EmployeeClient(HttpClient http) => _http = http;

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            var res = await _http.GetFromJsonAsync<IEnumerable<Employee>>("api/Employee");
            return res ?? Enumerable.Empty<Employee>();
        }

        public async Task<Employee?> GetByIdAsync(int? id)
        {
            var resp = await _http.GetAsync($"api/Employee/{id}");
            if (resp.StatusCode == HttpStatusCode.NotFound) return null;
            resp.EnsureSuccessStatusCode();
            return await resp.Content.ReadFromJsonAsync<Employee>();
        }

        public async Task AddAsync(Employee employee)
        {
            var resp = await _http.PostAsJsonAsync("api/Employee", employee);
            resp.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(Employee employee)
        {
            var resp = await _http.PutAsJsonAsync($"api/Employee/{employee.EmployeeId}", employee);
            resp.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var resp = await _http.DeleteAsync($"api/Employee/{id}");
            resp.EnsureSuccessStatusCode();
        }

        public async Task OnboardFromCandidateAsync(int candidateId)
        {
            var resp = await _http.PostAsync($"api/Employee/onboard/{candidateId}", null);
            resp.EnsureSuccessStatusCode();
        }
    }
}