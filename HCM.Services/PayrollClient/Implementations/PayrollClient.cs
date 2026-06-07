using HCM.Models.Models;
using HCM.Models.ViewModels;
using HCM.Services.PayrollClient.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.PayrollClient.Implementations
{
    public class PayrollClient : IPayrollClient
    {
        private readonly HttpClient _http;
        public PayrollClient(HttpClient http) => _http = http;

        public async Task<IEnumerable<PayComponent>> GetAllAsync()
        {
            var res = await _http.GetFromJsonAsync<IEnumerable<PayComponent>>("api/PayComponent");
            return res ?? Enumerable.Empty<PayComponent>();
        }

        public Task<IEnumerable<EmployeePay>> GetEmployeePayComponents()
        {
           var res = _http.GetFromJsonAsync<IEnumerable<EmployeePay>>("api/PayComponent/EmployeePays");
            return res ?? Task.FromResult(Enumerable.Empty<EmployeePay>());
        }

        public Task<IEnumerable<PayComponentModel>> GetEmployeePaysAsync(int employeeId)
        {            var res = _http.GetFromJsonAsync<IEnumerable<PayComponentModel>>($"api/PayComponent/Employee/{employeeId}/Pays");

            return res ?? Task.FromResult(Enumerable.Empty<PayComponentModel>());
        }

        public async Task UpsertAsync(PayComponent dto)
        {
            await _http.PostAsJsonAsync("api/PayComponent", dto);
        }
    }
}
