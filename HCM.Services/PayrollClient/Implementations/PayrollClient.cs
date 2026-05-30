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
    public class PayrollClient : IPayrollClient
    {
        private readonly HttpClient _http;
        public PayrollClient(HttpClient http) => _http = http;

        public async Task<IEnumerable<PayComponent>> GetAllAsync()
        {
            var res = await _http.GetFromJsonAsync<IEnumerable<PayComponent>>("api/PayComponent");
            return res ?? Enumerable.Empty<PayComponent>();
        }

        public async Task UpsertAsync(PayComponent dto)
        {
            await _http.PostAsJsonAsync("api/PayComponent", dto);
        }
    }
}
