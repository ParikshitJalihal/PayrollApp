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
    public class AccountClient : IAccountClient
    {
        private readonly HttpClient _http;
        public AccountClient(HttpClient http) => _http = http;

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            var res = await _http.GetFromJsonAsync<IEnumerable<Account>>("api/Accounts");
            return res ?? Enumerable.Empty<Account>();
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            var response = await _http.GetAsync($"api/Accounts/{id}");
            if (response.StatusCode == HttpStatusCode.NotFound) return null;
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Account>();
        }

        public async Task AddAsync(Account account)
        {
            var response = await _http.PostAsJsonAsync("api/Accounts", account);
            response.EnsureSuccessStatusCode();
            var created = await response.Content.ReadFromJsonAsync<Account>();
            if (created != null) account.Id = created.Id;
        }

        public async Task UpdateAsync(Account account)
        {
            var response = await _http.PutAsJsonAsync($"api/Accounts/{account.Id}", account);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/Accounts/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}